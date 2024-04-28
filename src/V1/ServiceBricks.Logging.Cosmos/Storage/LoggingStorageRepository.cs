using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// This is the storage repository for the Log module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public class LoggingStorageRepository<TDomain> : EntityFrameworkCoreStorageRepository<TDomain>
        where TDomain : class, IEntityFrameworkCoreDomainObject<TDomain>, new()
    {
        public LoggingStorageRepository(
            ILoggerFactory logFactory,
            LoggingCosmosContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}