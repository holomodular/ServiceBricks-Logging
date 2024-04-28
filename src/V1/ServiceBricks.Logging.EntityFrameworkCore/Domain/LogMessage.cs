using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// This is an application log message.
    /// </summary>
    public class LogMessage : EntityFrameworkCoreDomainObject<LogMessage>, IDpCreateDate
    {
        public long Key { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string Application { get; set; }
        public string Server { get; set; }
        public string Category { get; set; }
        public string UserStorageKey { get; set; }
        public string Path { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }

        public override IQueryable<LogMessage> DomainGetIQueryableDefaults(IQueryable<LogMessage> query)
        {
            return query.OrderByDescending(x => x.CreateDate);
        }

        public override Expression<Func<LogMessage, bool>> DomainGetItemFilter(LogMessage obj)
        {
            return x => x.Key == obj.Key;
        }
    }
}