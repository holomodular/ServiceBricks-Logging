using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// LogMessage domain object.
    /// </summary>
    public partial class LogMessage : EntityFrameworkCoreDomainObject<LogMessage>, IDpCreateDate
    {
        /// <summary>
        /// Internal Primary Key.
        /// </summary>
        public long Key { get; set; }

        /// <summary>
        /// The creation date of the log message.
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The application that generated the log message.
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// The server that generated the log message.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The category of the log message.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The user storage key.
        /// </summary>
        public string UserStorageKey { get; set; }

        /// <summary>
        /// The path of the log message.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The severity level of the message.
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// The message of the log.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The exception message, if any.
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// Additional properties of the log message.
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// Provide any defaults for the IQueryable object.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override IQueryable<LogMessage> DomainGetIQueryableDefaults(IQueryable<LogMessage> query)
        {
            return query.OrderByDescending(x => x.CreateDate);
        }

        /// <summary>
        /// Provide an expression that will filter an object based on its primary key.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override Expression<Func<LogMessage, bool>> DomainGetItemFilter(LogMessage obj)
        {
            return x => x.Key == obj.Key;
        }
    }
}