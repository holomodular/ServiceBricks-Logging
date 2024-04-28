using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    //[Index(nameof(CreateDate), nameof(RequestMethod), nameof(RequestPath), nameof(ResponseStatusCode), nameof(RequestUserId), Name = "IX_Logging_WebRequestMessage_CreateDate_RequestMethod_RequestPath_ResponseStatusCode_RequestUserId")]
    public class WebRequestMessage : EntityFrameworkCoreDomainObject<WebRequestMessage>, IDpCreateDate
    {
        public long Key { get; set; }
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
        public Guid? RequestUserId { get; set; }
        public int? ResponseStatusCode { get; set; }
        public string ResponseHeaders { get; set; }
        public string ResponseCookies { get; set; }
        public string ResponseContentType { get; set; }
        public long? ResponseContentLength { get; set; }
        public long? ResponseTotalMilliseconds { get; set; }
        public string ResponseBody { get; set; }

        public override IQueryable<WebRequestMessage> DomainGetIQueryableDefaults(IQueryable<WebRequestMessage> query)
        {
            return query.OrderByDescending(x => x.CreateDate);
        }

        public override Expression<Func<WebRequestMessage, bool>> DomainGetItemFilter(WebRequestMessage obj)
        {
            return x => x.Key == obj.Key;
        }
    }
}