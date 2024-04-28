namespace ServiceBricks.Logging
{
    public class WebRequestMessageOptions
    {
        public WebRequestMessageOptions()
        {
            ExcludeRequestPaths = new List<string>();
        }

        public bool EnableLogging { get; set; }
        public bool EnableRequestIPAddress { get; set; }
        public bool EnableRequestBody { get; set; }
        public bool EnableRequestBodyOnError { get; set; }
        public bool EnableRequestProtocol { get; set; }
        public bool EnableRequestScheme { get; set; }
        public bool EnableRequestMethod { get; set; }
        public bool EnableRequestPath { get; set; }
        public bool EnableRequestPathBase { get; set; }
        public bool EnableRequestQueryString { get; set; }
        public bool EnableRequestQuery { get; set; }
        public bool EnableRequestRouteValues { get; set; }
        public bool EnableRequestHost { get; set; }
        public bool EnableRequestHasFormContentType { get; set; }
        public bool EnableRequestCookies { get; set; }
        public bool EnableRequestContentType { get; set; }
        public bool EnableRequestContentLength { get; set; }
        public bool EnableRequestHeaders { get; set; }
        public bool EnableRequestIsHttps { get; set; }
        public bool EnableRequestUserId { get; set; }
        public bool EnableResponseStatusCode { get; set; }
        public bool EnableResponseHeaders { get; set; }
        public bool EnableResponseCookies { get; set; }
        public bool EnableResponseContentType { get; set; }
        public bool EnableResponseContentLength { get; set; }
        public bool EnableResponseTotalMilliseconds { get; set; }
        public bool EnableResponseBody { get; set; }
        public bool EnableExcludeRequestPaths { get; set; }
        public List<string> ExcludeRequestPaths { get; set; }
    }
}