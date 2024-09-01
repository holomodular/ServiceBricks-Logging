namespace ServiceBricks.Logging
{
    /// <summary>
    /// These are constants for the ServiceBricks Logging module.
    /// </summary>
    public static partial class LoggingConstants
    {
        /// <summary>
        /// Application settings keys for client API configuration.
        /// </summary>
        public const string APPSETTING_CLIENT_APICONFIG = @"ServiceBricks:Logging:Client:Api";

        /// <summary>
        /// Application settings keys for web request message configuration.
        /// </summary>
        public const string APPSETTING_WEBREQUESTMESSAGE = @"ServiceBricks:Logging:WebRequestMessage";

        /// <summary>
        /// Property names in state dictionary.
        /// </summary>
        public static partial class MiddlewareStateProperty
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