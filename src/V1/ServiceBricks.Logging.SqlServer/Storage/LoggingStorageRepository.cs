using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.SqlServer
{
    /// <summary>
    /// This is the storage repository for the ServiceBricks.Logging.SqlServer module.
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
            LoggingSqlServerContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}