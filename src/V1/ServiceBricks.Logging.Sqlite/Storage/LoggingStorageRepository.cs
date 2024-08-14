using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Sqlite
{
    /// <summary>
    /// This is the storage repository for the ServiceBricks.Logging.Sqlite module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public partial class LoggingStorageRepository<TDomain> : EntityFrameworkCoreStorageRepository<TDomain>
        where TDomain : class, IEntityFrameworkCoreDomainObject<TDomain>, new()
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="context"></param>
        public LoggingStorageRepository(
            ILoggerFactory logFactory,
            LoggingSqliteContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}