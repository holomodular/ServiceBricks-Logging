using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    public partial class WebRequestMessage : EntityFrameworkCoreDomainObject<WebRequestMessage>, IDpCreateDate
    {
        /// <summary>
        /// Internal Primary Key.
        /// </summary>
        public long Key { get; set; }

        /// <summary>
        /// The date and time the message was created in UTC.
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The Request IP Address.
        /// </summary>
        public string RequestIPAddress { get; set; }

        /// <summary>
        /// The Request Protocol.
        /// </summary>
        public string RequestProtocol { get; set; }

        /// <summary>
        /// The Request Scheme.
        /// </summary>
        public string RequestScheme { get; set; }

        /// <summary>
        /// The Request Method.
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// The Request Body.
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// The Request Path.
        /// </summary>
        public string RequestPath { get; set; }

        /// <summary>
        /// The Request Path Base.
        /// </summary>
        public string RequestPathBase { get; set; }

        /// <summary>
        /// The Request Query String.
        /// </summary>
        public string RequestQueryString { get; set; }

        /// <summary>
        /// The Request Query.
        /// </summary>
        public string RequestQuery { get; set; }

        /// <summary>
        /// The Request Route Values.
        /// </summary>
        public string RequestRouteValues { get; set; }

        /// <summary>
        /// The Request Host.
        /// </summary>
        public string RequestHost { get; set; }

        /// <summary>
        /// Determine if the Request Content Type is Form.
        /// </summary>
        public bool? RequestHasFormContentType { get; set; }

        /// <summary>
        /// The Request Cookies.
        /// </summary>
        public string RequestCookies { get; set; }

        /// <summary>
        /// The Request Content Type.
        /// </summary>
        public string RequestContentType { get; set; }

        /// <summary>
        /// The Request Content Length.
        /// </summary>
        public long? RequestContentLength { get; set; }

        /// <summary>
        /// The Request Headers.
        /// </summary>
        public string RequestHeaders { get; set; }

        /// <summary>
        /// Determine if the Request Is Https.
        /// </summary>
        public bool? RequestIsHttps { get; set; }

        /// <summary>
        /// The User storage key associated with the Request.
        /// </summary>
        public string RequestUserStorageKey { get; set; }

        /// <summary>
        /// The status code of the response.
        /// </summary>
        public int? ResponseStatusCode { get; set; }

        /// <summary>
        /// The Response Headers.
        /// </summary>
        public string ResponseHeaders { get; set; }

        /// <summary>
        /// The Response Cookies.
        /// </summary>
        public string ResponseCookies { get; set; }

        /// <summary>
        /// The Response Content Type.
        /// </summary>
        public string ResponseContentType { get; set; }

        /// <summary>
        /// The Response Content Length.
        /// </summary>
        public long? ResponseContentLength { get; set; }

        /// <summary>
        /// The Response Total Milliseconds.
        /// </summary>
        public long? ResponseTotalMilliseconds { get; set; }

        /// <summary>
        /// The Response Body.
        /// </summary>
        public string ResponseBody { get; set; }

        /// <summary>
        /// The exception that occured.
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// Provide any defaults for the IQueryable object.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override IQueryable<WebRequestMessage> DomainGetIQueryableDefaults(IQueryable<WebRequestMessage> query)
        {
            return query.OrderByDescending(x => x.CreateDate);
        }

        /// <summary>
        /// Provide an expression that will filter an object based on its primary key.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override Expression<Func<WebRequestMessage, bool>> DomainGetItemFilter(WebRequestMessage obj)
        {
            return x => x.Key == obj.Key;
        }
    }
}