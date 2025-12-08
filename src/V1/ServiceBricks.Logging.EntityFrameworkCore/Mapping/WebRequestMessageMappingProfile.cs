namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// This is an mapping profile for the WebRequestMessage domain object.
    /// </summary>
    public partial class WebRequestMessageMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<WebRequestMessage, WebRequestMessageDto>(
                (s, d) =>
                {
                    d.Application = s.Application;
                    d.CreateDate = s.CreateDate;
                    d.Exception = s.Exception;
                    d.RequestBody = s.RequestBody;
                    d.RequestContentLength = s.RequestContentLength;
                    d.RequestContentType = s.RequestContentType;
                    d.RequestCookies = s.RequestCookies;
                    d.RequestHasFormContentType = s.RequestHasFormContentType;
                    d.RequestHeaders = s.RequestHeaders;
                    d.RequestHost = s.RequestHost;
                    d.RequestIPAddress = s.RequestIPAddress;
                    d.RequestIsHttps = s.RequestIsHttps;
                    d.RequestMethod = s.RequestMethod;
                    d.RequestPath = s.RequestPath;
                    d.RequestPathBase = s.RequestPathBase;
                    d.RequestProtocol = s.RequestProtocol;
                    d.RequestQuery = s.RequestQuery;
                    d.RequestQueryString = s.RequestQueryString;
                    d.RequestRouteValues = s.RequestRouteValues;
                    d.RequestScheme = s.RequestScheme;
                    d.ResponseBody = s.ResponseBody;
                    d.ResponseContentLength = s.ResponseContentLength;
                    d.ResponseContentType = s.ResponseContentType;
                    d.ResponseCookies = s.ResponseCookies;
                    d.ResponseHeaders = s.ResponseHeaders;
                    d.ResponseStatusCode = s.ResponseStatusCode;
                    d.ResponseTotalMilliseconds = s.ResponseTotalMilliseconds;
                    d.Server = s.Server;
                    d.StorageKey = s.Key.ToString();
                    d.UserStorageKey = s.UserStorageKey;
                });

            registry.Register<WebRequestMessageDto, WebRequestMessage>(
                (s, d) =>
                {
                    d.Application = s.Application;
                    //d.CreateDate ignore by rule
                    d.Exception = s.Exception;
                    d.RequestBody = s.RequestBody;
                    d.RequestContentLength = s.RequestContentLength;
                    d.RequestContentType = s.RequestContentType;
                    d.RequestCookies = s.RequestCookies;
                    d.RequestHasFormContentType = s.RequestHasFormContentType;
                    d.RequestHeaders = s.RequestHeaders;
                    d.RequestHost = s.RequestHost;
                    d.RequestIPAddress = s.RequestIPAddress;
                    d.RequestIsHttps = s.RequestIsHttps;
                    d.RequestMethod = s.RequestMethod;
                    d.RequestPath = s.RequestPath;
                    d.RequestPathBase = s.RequestPathBase;
                    d.RequestProtocol = s.RequestProtocol;
                    d.RequestQuery = s.RequestQuery;
                    d.RequestQueryString = s.RequestQueryString;
                    d.RequestRouteValues = s.RequestRouteValues;
                    d.RequestScheme = s.RequestScheme;
                    d.ResponseBody = s.ResponseBody;
                    d.ResponseContentLength = s.ResponseContentLength;
                    d.ResponseContentType = s.ResponseContentType;
                    d.ResponseCookies = s.ResponseCookies;
                    d.ResponseHeaders = s.ResponseHeaders;
                    d.ResponseStatusCode = s.ResponseStatusCode;
                    d.ResponseTotalMilliseconds = s.ResponseTotalMilliseconds;
                    d.Server = s.Server;
                    long tempKey;
                    if (long.TryParse(s.StorageKey, out tempKey))
                        d.Key = tempKey;
                    d.UserStorageKey = s.UserStorageKey;
                });
        }
    }
}