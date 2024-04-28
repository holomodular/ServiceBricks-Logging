using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StateProp = ServiceBricks.Logging.LoggingConstants.MiddlewareStateProperty;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is middleware used by the custom database logger.
    /// </summary>
    public class CustomLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomLoggerMiddleware> _logger;

        public CustomLoggerMiddleware(RequestDelegate next, ILogger<CustomLoggerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var logState = GetStateObject(httpContext);
            using (_logger.BeginScope(logState))
            {
                await _next(httpContext);
            }
        }

        public virtual Dictionary<string, object> GetStateObject(HttpContext httpContext)
        {
            var logState = new Dictionary<string, object>();
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

        protected string GetUserStorageKey(HttpContext context)
        {
            return context?.User?.Claims?.Where(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
        }

        protected string GetUsername(HttpContext context)
        {
            return context?.User?.Identity?.Name;
        }

        protected string GetStatusCode(HttpContext context)
        {
            return context?.Response.StatusCode.ToString();
        }

        protected string GetPath(HttpContext context)
        {
            return context?.Request?.Path.Value;
        }

        protected string GetMethod(HttpContext context)
        {
            return context?.Request?.Method;
        }

        protected Dictionary<string, string> GetQueryString(HttpContext context)
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

        protected Dictionary<string, string> GetForm(HttpContext context)
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

        protected string GetContentType(HttpContext context)
        {
            return context?.Request?.ContentType;
        }

        protected string GetProtocol(HttpContext context)
        {
            return context?.Request?.Protocol;
        }

        protected Dictionary<string, string> GetHeaders(HttpContext context)
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