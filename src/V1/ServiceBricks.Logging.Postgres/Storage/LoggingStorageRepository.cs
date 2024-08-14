using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Postgres
{
    /// <summary>
    /// This is the storage repository for the ServiceBricks Logging Postgres module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public partial class LoggingStorageRepository<TDomain> : EntityFrameworkCoreStorageRepository<TDomain>
        where TDomain : class, IEntityFrameworkCoreDomainObject<TDomain>, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="context"></param>
        public LoggingStorageRepository(
            ILoggerFactory logFactory,
            LoggingPostgresContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}