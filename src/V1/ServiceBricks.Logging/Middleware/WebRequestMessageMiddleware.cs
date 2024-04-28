using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceBricks.Logging
{
    public class WebRequestMessageMiddleware : IMiddleware
    {
        private readonly ILogger<WebRequestMessageMiddleware> _logger;
        private readonly IWebRequestMessageApiService _webRequestMessageApiService;
        private readonly WebRequestMessageOptions _webRequestOptions;

        private Stopwatch _watch = null;
        private string _requestBody = null;
        private string _responseBody = null;

        public WebRequestMessageMiddleware(
            IWebRequestMessageApiService webRequestMessageApiService,
            ILogger<WebRequestMessageMiddleware> logger,
            IOptions<WebRequestMessageOptions> webRequestOptions)
        {
            _webRequestMessageApiService = webRequestMessageApiService;
            _logger = logger;
            _webRequestOptions = webRequestOptions.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _watch = Stopwatch.StartNew();
            _requestBody = null;
            _responseBody = null;
            _requestBody = await GetRequestBody(context.Request);
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    await WriteMessage(context, ex);
                    throw;
                }
                await WriteMessage(context, null);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task WriteMessage(HttpContext context, Exception exception)
        {
            _responseBody = await GetResponseBody(context.Response);
            _watch.Stop();
            string userId = null;
            if (_webRequestOptions.EnableRequestUserId && context.User != null && context.User.Claims != null)
            {
                var claim = context.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).FirstOrDefault();
                if (claim != null)
                    userId = claim.Value;
            }

            bool logMessage = _webRequestOptions.EnableLogging;
            if (_webRequestOptions.EnableExcludeRequestPaths && context.Request.Path.HasValue && _webRequestOptions.ExcludeRequestPaths != null && _webRequestOptions.ExcludeRequestPaths.Count > 0)
            {
                foreach (var exclude in _webRequestOptions.ExcludeRequestPaths)
                {
                    try
                    {
                        if (Regex.Match(context.Request.Path.Value, exclude).Success)
                            logMessage = false;
                    }
                    catch { }
                    if (context.Request.Path.Value.StartsWith(exclude, StringComparison.InvariantCultureIgnoreCase))
                        logMessage = false;
                }
            }

            if (logMessage)
            {
                long? responseContentLength = null;
                if (_webRequestOptions.EnableResponseContentLength && context.Response.ContentLength.HasValue)
                    responseContentLength = context.Response.ContentLength.Value;
                else if (_webRequestOptions.EnableResponseContentLength && !string.IsNullOrEmpty(_responseBody))
                    responseContentLength = _responseBody.Length;

                int? statusCode = null;
                if (_webRequestOptions.EnableResponseStatusCode)
                {
                    statusCode = context.Response.StatusCode;
                    if (exception != null)
                        statusCode = 500;
                }

                string ipaddress = null;
                if (_webRequestOptions.EnableRequestIPAddress && context.Connection != null)
                {
                    ipaddress = context.Connection.RemoteIpAddress.ToString();
                    if (ipaddress == LoggingConstants.IPADDRESS_LOCAL_SHORT)
                        ipaddress = LoggingConstants.IPADDRESS_LOCAL_FULL;
                }

                await _webRequestMessageApiService.CreateAsync(new WebRequestMessageDto()
                {
                    CreateDate = DateTimeOffset.UtcNow,
                    RequestIPAddress = ipaddress,
                    RequestBody = _webRequestOptions.EnableRequestBody || (_webRequestOptions.EnableRequestBodyOnError && exception != null) ? _requestBody : null,
                    RequestContentLength = _webRequestOptions.EnableRequestContentLength ? context.Request.ContentLength : null,
                    RequestContentType = _webRequestOptions.EnableRequestContentType ? context.Request.ContentType : null,
                    RequestCookies = _webRequestOptions.EnableRequestCookies && context.Request.Cookies != null ? JsonConvert.SerializeObject(context.Request.Cookies) : null,
                    RequestHasFormContentType = _webRequestOptions.EnableRequestHasFormContentType ? context.Request.HasFormContentType : new Nullable<bool>(),
                    RequestHeaders = _webRequestOptions.EnableRequestHeaders && context.Request.Headers != null ? JsonConvert.SerializeObject(context.Request.Headers) : null,
                    RequestHost = _webRequestOptions.EnableRequestHost ? JsonConvert.SerializeObject(context.Request.Host) : null,
                    RequestIsHttps = _webRequestOptions.EnableRequestIsHttps ? context.Request.IsHttps : new Nullable<bool>(),
                    RequestMethod = _webRequestOptions.EnableRequestMethod ? context.Request.Method : null,
                    RequestPath = _webRequestOptions.EnableRequestPath && context.Request.Path.HasValue ? context.Request.Path.Value : null,
                    RequestPathBase = _webRequestOptions.EnableRequestPathBase && context.Request.PathBase.HasValue ? context.Request.PathBase.Value : null,
                    RequestProtocol = _webRequestOptions.EnableRequestProtocol ? context.Request.Protocol : null,
                    RequestQuery = _webRequestOptions.EnableRequestQuery && context.Request.Query != null ? JsonConvert.SerializeObject(context.Request.Query) : null,
                    RequestQueryString = _webRequestOptions.EnableRequestQueryString && context.Request.QueryString.HasValue ? context.Request.QueryString.Value : null,
                    RequestRouteValues = _webRequestOptions.EnableRequestRouteValues && context.Request.RouteValues != null ? JsonConvert.SerializeObject(context.Request.RouteValues) : null,
                    RequestScheme = _webRequestOptions.EnableRequestScheme ? context.Request.Scheme : null,
                    RequestUserId = _webRequestOptions.EnableRequestUserId ? userId : null,
                    ResponseBody = _webRequestOptions.EnableResponseBody ? _responseBody : null,
                    ResponseContentLength = responseContentLength,
                    ResponseContentType = _webRequestOptions.EnableResponseContentType ? context.Response.ContentType : null,
                    ResponseCookies = _webRequestOptions.EnableResponseCookies && context.Response.Cookies != null ? JsonConvert.SerializeObject(context.Response.Cookies) : null,
                    ResponseHeaders = _webRequestOptions.EnableResponseHeaders && context.Response.Headers != null ? JsonConvert.SerializeObject(context.Response.Headers) : null,
                    ResponseStatusCode = statusCode,
                    ResponseTotalMilliseconds = _webRequestOptions.EnableResponseTotalMilliseconds ? _watch.ElapsedMilliseconds : new Nullable<long>()
                });
            }
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            if (request.Body.CanSeek)
                request.Body.Position = 0;
            return bodyAsText;
        }

        private async Task<string> GetResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}