namespace ServiceBricks.Logging
{
    /// <summary>
    /// Options class for WebRequestMessage configurations.
    /// </summary>
    public partial class WebRequestMessageOptions
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public WebRequestMessageOptions()
        {
            ExcludeRequestPaths = new List<string>();
        }

        /// <summary>
        /// Determines if logging is enabled overall. If false, no logging will occur.
        /// </summary>
        public bool EnableLogging { get; set; }

        /// <summary>
        /// Determines if logging is enabled for local IP addresses. If false, no logging will occur for local IP addresses requests.
        /// </summary>
        public bool EnableLocalIpRequests { get; set; }

        /// <summary>
        /// Determines if logging is enabled for remote IP addresses. If false, the IP Address will not be stored.
        /// </summary>
        public bool EnableRequestIPAddress { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request body. If false, the request body will not be stored.
        /// </summary>
        public bool EnableRequestBody { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request body on error. If false, the request body will not be stored on error.
        /// </summary>
        public bool EnableRequestBodyOnError { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request protocol. If false, the protocol will not be stored.
        /// </summary>
        public bool EnableRequestProtocol { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request scheme. If false, the scheme will not be stored.
        /// </summary>
        public bool EnableRequestScheme { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request method. If false, the method will not be stored.
        /// </summary>
        public bool EnableRequestMethod { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request path. If false, the path will not be stored.
        /// </summary>
        public bool EnableRequestPath { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request path base. If false, the path base will not be stored.
        /// </summary>
        public bool EnableRequestPathBase { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request query string. If false, the query string will not be stored.
        /// </summary>
        public bool EnableRequestQueryString { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request query values. If false, the query values will not be stored.
        /// </summary>
        public bool EnableRequestQuery { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request route values. If false, the route values will not be stored.
        /// </summary>
        public bool EnableRequestRouteValues { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request host. If false, the host will not be stored.
        /// </summary>
        public bool EnableRequestHost { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request form content type. If false, the form content type will not be stored.
        /// </summary>
        public bool EnableRequestHasFormContentType { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request cookies. If false, the cookies will not be stored.
        /// </summary>
        public bool EnableRequestCookies { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request content type. If false, the content type will not be stored.
        /// </summary>
        public bool EnableRequestContentType { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request content length. If false, the content length will not be stored.
        /// </summary>
        public bool EnableRequestContentLength { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request headers. If false, the headers will not be stored.
        /// </summary>
        public bool EnableRequestHeaders { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request is HTTPS. If false, the is HTTPS will not be stored.
        /// </summary>
        public bool EnableRequestIsHttps { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the request user storage key. If false, the user storage key will not be stored.
        /// </summary>
        public bool EnableRequestUserId { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the response status code. If false, the status code will not be stored.
        /// </summary>
        public bool EnableResponseStatusCode { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the response headers. If false, the headers will not be stored.
        /// </summary>
        public bool EnableResponseHeaders { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the response cookies. If false, the cookies will not be stored.
        /// </summary>
        public bool EnableResponseCookies { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the response content type. If false, the content type will not be stored.
        /// </summary>
        public bool EnableResponseContentType { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the response content length. If false, the content length will not be stored.
        /// </summary>
        public bool EnableResponseContentLength { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the response total milliseconds. If false, the total milliseconds will not be stored.
        /// </summary>
        public bool EnableResponseTotalMilliseconds { get; set; }

        /// <summary>
        /// Determines if logging is enabled for the response body. If false, the body will not be stored.
        /// </summary>
        public bool EnableResponseBody { get; set; }

        /// <summary>
        /// Determines if logging will exclude any request paths. If false, no paths will be excluded.
        /// </summary>
        public bool EnableExcludeRequestPaths { get; set; }

        /// <summary>
        /// Determines if logging will exclude any request paths using regex expressions. If false, no regex comparisons will be used.
        /// </summary>
        public bool EnableExcludeRegExExpressions { get; set; }

        /// <summary>
        /// The list of excluded request paths.
        /// </summary>
        public List<string> ExcludeRequestPaths { get; set; }

        /// <summary>
        /// Determine if exceptions will be logged. If false, the exception will not be logged.
        /// </summary>
        public bool EnableExceptions { get; set; }
    }
}