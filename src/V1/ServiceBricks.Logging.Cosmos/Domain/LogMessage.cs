using ServiceBricks.Storage.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// This is an application log message.
    /// </summary>
    public partial class LogMessage : EntityFrameworkCoreDomainObject<LogMessage>, IDpCreateDate
    {
        /// <summary>
        /// Internal Primary Key.
        /// </summary>
        public Guid Key { get; set; }

        /// <summary>
        /// Internal Partition Key
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// The date and time the log message was created in UTC.
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The name of the application that created the log message.
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// The name of the server that created the log message.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The category name that the log message belongs to.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The user storage key associated to the log message.
        /// </summary>
        public string UserStorageKey { get; set; }

        /// <summary>
        /// The URI path of the request of the log message.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The severity level of the the log message.
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// The message of the log message.
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