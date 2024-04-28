using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
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
            LoggingInMemoryContext context)
            : base(logFactory)
        {
            Context = context;
            DbSet = context.Set<TDomain>();
        }
    }
}