using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a middleware for logging web requests.
    /// </summary>
    public sealed class WebRequestMessageMiddleware : IMiddleware
    {
        private readonly IWebRequestMessageApiService _webRequestMessageApiService;
        private readonly WebRequestMessageOptions _webRequestOptions;
        private readonly ApplicationOptions _applicationOptions;

        private Stopwatch _watch = null;
        private string _requestBody = null;
        private string _responseBody = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="webRequestMessageApiService"></param>
        /// <param name="webRequestOptions"></param>
        /// <param name="applicationOptions"></param>
        public WebRequestMessageMiddleware(
            IWebRequestMessageApiService webRequestMessageApiService,
            IOptions<WebRequestMessageOptions> webRequestOptions,
            IOptions<ApplicationOptions> applicationOptions)
        {
            _webRequestMessageApiService = webRequestMessageApiService;
            _webRequestOptions = webRequestOptions.Value;
            _applicationOptions = applicationOptions.Value;
        }

        /// <summary>
        /// This method is called by the runtime. Use this method to add middleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // AI: Check if we should log at all
            if (!_webRequestOptions.EnableLogging)
            {
                await next(context);
                return;
            }

            // AI: Get the request and response to write the message
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
                    await responseBody.CopyToAsync(originalBodyStream);
                    return;
                }
                await WriteMessage(context, null);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        /// <summary>
        /// Write the message to the WebRequestMessage API service.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task WriteMessage(HttpContext context, Exception exception)
        {
            _responseBody = await GetResponseBody(context.Response);
            _watch.Stop();

            // AI: Check options if request path is excluded, if so return and do not log message
            if (context.Request.Path.HasValue && _webRequestOptions.ExcludeRequestPaths != null && _webRequestOptions.ExcludeRequestPaths.Count > 0)
            {
                foreach (var exclude in _webRequestOptions.ExcludeRequestPaths)
                {
                    // AI: Check if we should exclude the path. If found, return and do not log message
                    if (context.Request.Path.Value.StartsWith(exclude, StringComparison.InvariantCultureIgnoreCase))
                        return;

                    // AI: Check if we should use regex expressions to exclude the path. If found, return and do not log message
                    if (_webRequestOptions.EnableExcludeRequestPathsRegExExpressions)
                    {
                        try
                        {
                            if (Regex.Match(context.Request.Path.Value, exclude).Success)
                                return;
                        }
                        catch { }
                    }
                }
            }

            // AI: Get IP Address
            string ipaddress = null;
            if (context.Connection != null && context.Connection.RemoteIpAddress != null)
            {
                ipaddress = context.Connection.RemoteIpAddress.ToString();

                // AI: Check options if we should log excluded IP address requests
                if (_webRequestOptions.ExcludeIpAddresses != null && _webRequestOptions.ExcludeIpAddresses.Count > 0)
                {
                    foreach (var exclude in _webRequestOptions.ExcludeIpAddresses)
                    {
                        // AI: Check if we should exclude the path. If found, return and do not log message
                        if (ipaddress == exclude)
                            return;

                        // AI: Check if we should use regex expressions to exclude the IP Address. If found, return and do not log message
                        if (_webRequestOptions.EnableExcludeIpAddressesRegExExpressions)
                        {
                            try
                            {
                                if (Regex.Match(ipaddress, exclude).Success)
                                    return;
                            }
                            catch { }
                        }
                    }
                }
            }

            // AI: Check options if we should log the user associated with the request
            string userId = null;
            if (_webRequestOptions.EnableUserStorageKey && context.User != null && context.User.Claims != null)
            {
                var claim = context.User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).FirstOrDefault();
                if (claim != null)
                    userId = claim.Value;
            }

            // AI: Get content length
            long? responseContentLength = null;
            if (context.Response.ContentLength.HasValue)
                responseContentLength = context.Response.ContentLength.Value;
            else if (!string.IsNullOrEmpty(_responseBody))
                responseContentLength = Encoding.UTF8.GetBytes(_responseBody).Length;

            // AI: Create record in the WebRequestMessageDto API service
            await _webRequestMessageApiService.CreateAsync(new WebRequestMessageDto()
            {
                CreateDate = DateTimeOffset.UtcNow,
                Application = _applicationOptions.Name,
                Server = System.Net.Dns.GetHostName(),
                UserStorageKey = _webRequestOptions.EnableUserStorageKey ? userId : null,
                RequestIPAddress = _webRequestOptions.EnableRequestIPAddress ? ipaddress : null,
                RequestBody = _webRequestOptions.EnableRequestBody || (_webRequestOptions.EnableRequestBodyOnError && exception != null) ? _requestBody : null,
                RequestContentLength = _webRequestOptions.EnableRequestContentLength ? context.Request.ContentLength : null,
                RequestContentType = _webRequestOptions.EnableRequestContentType ? context.Request.ContentType : null,
                RequestCookies = _webRequestOptions.EnableRequestCookies && context.Request.Cookies != null ? JsonConvert.SerializeObject(context.Request.Cookies) : null,
                RequestHasFormContentType = _webRequestOptions.EnableRequestHasFormContentType ? context.Request.HasFormContentType : new Nullable<bool>(),
                RequestHeaders = _webRequestOptions.EnableRequestHeaders && context.Request.Headers != null ? JsonConvert.SerializeObject(context.Request.Headers) : null,
                RequestHost = _webRequestOptions.EnableRequestHost ? JsonConvert.SerializeObject(context.Request.Host) : null,
                RequestIsHttps = _webRequestOptions.EnableRequestIsHttps ? context.Request.IsHttps : null,
                RequestMethod = _webRequestOptions.EnableRequestMethod ? context.Request.Method : null,
                RequestPath = _webRequestOptions.EnableRequestPath && context.Request.Path.HasValue ? context.Request.Path.Value : null,
                RequestPathBase = _webRequestOptions.EnableRequestPathBase && context.Request.PathBase.HasValue ? context.Request.PathBase.Value : null,
                RequestProtocol = _webRequestOptions.EnableRequestProtocol ? context.Request.Protocol : null,
                RequestQuery = _webRequestOptions.EnableRequestQuery && context.Request.Query != null ? JsonConvert.SerializeObject(context.Request.Query) : null,
                RequestQueryString = _webRequestOptions.EnableRequestQueryString && context.Request.QueryString.HasValue ? context.Request.QueryString.Value : null,
                RequestRouteValues = _webRequestOptions.EnableRequestRouteValues && context.Request.RouteValues != null ? JsonConvert.SerializeObject(context.Request.RouteValues) : null,
                RequestScheme = _webRequestOptions.EnableRequestScheme ? context.Request.Scheme : null,
                ResponseBody = _webRequestOptions.EnableResponseBody ? _responseBody : null,
                ResponseContentLength = _webRequestOptions.EnableResponseContentLength ? responseContentLength : null,
                ResponseContentType = _webRequestOptions.EnableResponseContentType ? context.Response.ContentType : null,
                ResponseCookies = _webRequestOptions.EnableResponseCookies && context.Response.Cookies != null ? JsonConvert.SerializeObject(context.Response.Cookies) : null,
                ResponseHeaders = _webRequestOptions.EnableResponseHeaders && context.Response.Headers != null ? JsonConvert.SerializeObject(context.Response.Headers) : null,
                ResponseStatusCode = _webRequestOptions.EnableResponseStatusCode ? context.Response.StatusCode : null,
                ResponseTotalMilliseconds = _webRequestOptions.EnableResponseTotalMilliseconds ? _watch.ElapsedMilliseconds : null,
                Exception = _webRequestOptions.EnableExceptions && exception != null ? exception.ToString() : null
            });
        }

        /// <summary>
        /// Get the request body.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> GetRequestBody(HttpRequest request)
        {
            // AI: Enable buffering to read the body
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            if (request.Body.CanSeek)
                request.Body.Position = 0;
            return bodyAsText;
        }

        /// <summary>
        /// Get the response body.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<string> GetResponseBody(HttpResponse response)
        {
            // AI: Get the response body
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}