namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a web request message data transer object.
    /// </summary>
    public partial class WebRequestMessageDto : DataTransferObject
    {
        /// <summary>
        /// The date and time the message was created in UTC.
        /// </summary>
        public virtual DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The name of the application that created the log message.
        /// </summary>
        public virtual string Application { get; set; }

        /// <summary>
        /// The name of the server that created the log message.
        /// </summary>
        public virtual string Server { get; set; }

        /// <summary>
        /// The User storage key associated with the Request.
        /// </summary>
        public virtual string UserStorageKey { get; set; }

        /// <summary>
        /// The Request IP Address.
        /// </summary>
        public virtual string RequestIPAddress { get; set; }

        /// <summary>
        /// The Request Protocol.
        /// </summary>
        public virtual string RequestProtocol { get; set; }

        /// <summary>
        /// The Request Scheme.
        /// </summary>
        public virtual string RequestScheme { get; set; }

        /// <summary>
        /// The Request Method.
        /// </summary>
        public virtual string RequestMethod { get; set; }

        /// <summary>
        /// The Request Body.
        /// </summary>
        public virtual string RequestBody { get; set; }

        /// <summary>
        /// The Request Path.
        /// </summary>
        public virtual string RequestPath { get; set; }

        /// <summary>
        /// The Request Path Base.
        /// </summary>
        public virtual string RequestPathBase { get; set; }

        /// <summary>
        /// The Request Query String.
        /// </summary>
        public virtual string RequestQueryString { get; set; }

        /// <summary>
        /// The Request Query.
        /// </summary>
        public virtual string RequestQuery { get; set; }

        /// <summary>
        /// The Request Route Values.
        /// </summary>
        public virtual string RequestRouteValues { get; set; }

        /// <summary>
        /// The Request Host.
        /// </summary>
        public virtual string RequestHost { get; set; }

        /// <summary>
        /// Determine if the Request Content Type is Form.
        /// </summary>
        public virtual bool? RequestHasFormContentType { get; set; }

        /// <summary>
        /// The Request Cookies.
        /// </summary>
        public virtual string RequestCookies { get; set; }

        /// <summary>
        /// The Request Content Type.
        /// </summary>
        public virtual string RequestContentType { get; set; }

        /// <summary>
        /// The Request Content Length.
        /// </summary>
        public virtual long? RequestContentLength { get; set; }

        /// <summary>
        /// The Request Headers.
        /// </summary>
        public virtual string RequestHeaders { get; set; }

        /// <summary>
        /// Determine if the Request Is Https.
        /// </summary>
        public virtual bool? RequestIsHttps { get; set; }

        /// <summary>
        /// The status code of the response.
        /// </summary>
        public virtual int? ResponseStatusCode { get; set; }

        /// <summary>
        /// The Response Headers.
        /// </summary>
        public virtual string ResponseHeaders { get; set; }

        /// <summary>
        /// The Response Cookies.
        /// </summary>
        public virtual string ResponseCookies { get; set; }

        /// <summary>
        /// The Response Content Type.
        /// </summary>
        public virtual string ResponseContentType { get; set; }

        /// <summary>
        /// The Response Content Length.
        /// </summary>
        public virtual long? ResponseContentLength { get; set; }

        /// <summary>
        /// The Response Total Milliseconds.
        /// </summary>
        public virtual long? ResponseTotalMilliseconds { get; set; }

        /// <summary>
        /// The Response Body.
        /// </summary>
        public virtual string ResponseBody { get; set; }

        /// <summary>
        /// The exception that occured.
        /// </summary>
        public virtual string Exception { get; set; }
    }
}