namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a log message data transer object.
    /// </summary>
    public class WebRequestMessageDto : DataTransferObject
    {
        public DateTimeOffset CreateDate { get; set; }
        public string RequestIPAddress { get; set; }
        public string RequestProtocol { get; set; }
        public string RequestScheme { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string RequestPath { get; set; }
        public string RequestPathBase { get; set; }
        public string RequestQueryString { get; set; }
        public string RequestQuery { get; set; }
        public string RequestRouteValues { get; set; }
        public string RequestHost { get; set; }
        public bool? RequestHasFormContentType { get; set; }
        public string RequestCookies { get; set; }
        public string RequestContentType { get; set; }
        public long? RequestContentLength { get; set; }
        public string RequestHeaders { get; set; }
        public bool? RequestIsHttps { get; set; }
        public string RequestUserId { get; set; }
        public int? ResponseStatusCode { get; set; }
        public string ResponseHeaders { get; set; }
        public string ResponseCookies { get; set; }
        public string ResponseContentType { get; set; }
        public long? ResponseContentLength { get; set; }
        public long? ResponseTotalMilliseconds { get; set; }
        public string ResponseBody { get; set; }
    }
}