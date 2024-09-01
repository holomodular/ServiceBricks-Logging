using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StateProp = ServiceBricks.Logging.LoggingConstants.MiddlewareStateProperty;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a custom logger middleware component used to add http request properties to the request pipeline.
    /// </summary>
    public sealed class LogMessageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMessageMiddleware> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public LogMessageMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LogMessageMiddleware>();
            _next = next;
        }

        /// <summary>
        /// This method is called by the runtime. Use this method to add middleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var logState = GetStateObject(httpContext);
            using (_logger.BeginScope(logState))
            {
                await _next(httpContext);
            }
        }

        /// <summary>
        /// This method is used to get the state object for the logger.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private Dictionary<string, object> GetStateObject(HttpContext httpContext)
        {
            var logState = new Dictionary<string, object>();

            // AI: Add properties to the log state object that we want to log
            logState.TryAdd(StateProp.IPADDRESS, IpAddressService.GetIPAddress(httpContext));
            logState.TryAdd(StateProp.USER_STORAGE_KEY, GetUserStorageKey(httpContext));
            logState.TryAdd(StateProp.USER_NAME, GetUsername(httpContext));
            logState.TryAdd(StateProp.STATUS_CODE, GetStatusCode(httpContext));
            logState.TryAdd(StateProp.PROTOCOL, GetProtocol(httpContext));
            logState.TryAdd(StateProp.METHOD, GetMethod(httpContext));
            logState.TryAdd(StateProp.PATH, GetPath(httpContext));
            logState.TryAdd(StateProp.QUERYSTRING, GetQueryString(httpContext));
            logState.TryAdd(StateProp.CONTENT_TYPE, GetContentType(httpContext));
            logState.TryAdd(StateProp.FORM, GetForm(httpContext));
            logState.TryAdd(StateProp.HEADERS, GetHeaders(httpContext));
            return logState;
        }

        /// <summary>
        /// Get the user storage key from the http context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetUserStorageKey(HttpContext context)
        {
            return context?.User?.Claims?.Where(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
        }

        /// <summary>
        /// Get the username from the http context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetUsername(HttpContext context)
        {
            return context?.User?.Identity?.Name;
        }

        /// <summary>
        /// Get the status code from the http context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetStatusCode(HttpContext context)
        {
            return context?.Response.StatusCode.ToString();
        }

        /// <summary>
        /// Get the path from the http context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetPath(HttpContext context)
        {
            return context?.Request?.Path.Value;
        }

        /// <summary>
        /// Get the method from the http context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetMethod(HttpContext context)
        {
            return context?.Request?.Method;
        }

        /// <summary>
        /// Get the query string from the http context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetQueryString(HttpContext context)
        {
            try
            {
                return context?.Request?.Query?.Keys.ToDictionary(x => x, x => context.Request.Query[x].ToString());
            }
            catch (Exception)
            {
                return new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// Get the form from the http context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetForm(HttpContext context)
        {
            try
            {
                return context?.Request?.Form?.Keys.ToDictionary(x => x, x => context.Request.Form[x].ToString());
            }
            catch (Exception)
            {
                return new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// Get the content type from the request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetContentType(HttpContext context)
        {
            return context?.Request?.ContentType;
        }

        /// <summary>
        /// Get the protocol from the request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetProtocol(HttpContext context)
        {
            return context?.Request?.Protocol;
        }

        /// <summary>
        /// Get the headers from the request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetHeaders(HttpContext context)
        {
            try
            {
                return context?.Request?.Headers?.Keys.ToDictionary(x => x, x => context.Request.Headers[x].ToString());
            }
            catch (Exception)
            {
                return new Dictionary<string, string>();
            }
        }
    }
}