namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class LoggingConstants
    {
        public const string APPSETTING_CLIENT_APICONFIG = @"ServiceBricks:Logging:Client:ApiConfig";
        public const string APPSETTING_WEBREQUESTMESSAGE = @"ServiceBricks:Logging:WebRequestMessage";

        public const string IPADDRESS_LOCAL_SHORT = "::1";
        public const string IPADDRESS_LOCAL_FULL = "127.0.0.1";

        /// <summary>
        /// Property names in state dictionary.
        /// </summary>
        public static class MiddlewareStateProperty
        {
            public const string IPADDRESS = "IpAddress";
            public const string USER_STORAGE_KEY = "UserStorageKey";
            public const string USER_NAME = "UserName";
            public const string STATUS_CODE = "StatusCode";
            public const string PROTOCOL = "Protocol";
            public const string METHOD = "Method";
            public const string PATH = "Path";
            public const string QUERYSTRING = "QueryString";
            public const string CONTENT_TYPE = "ContentType";
            public const string FORM = "Form";
            public const string HEADERS = "Headers";
        }
    }
}