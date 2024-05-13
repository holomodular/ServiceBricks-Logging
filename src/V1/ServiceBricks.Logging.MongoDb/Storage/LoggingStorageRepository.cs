using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is the storage repository for the Log module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public class LoggingStorageRepository<TDomain> : MongoDbStorageRepository<TDomain>
        where TDomain : class, IMongoDbDomainObject<TDomain>, new()
    {
        public LoggingStorageRepository(
            ILoggerFactory logFactory,
            IConfiguration configuration)
            : base(logFactory)
        {
            ConnectionString = configuration.GetMongoDbConnectionString(
                LoggingMongoDbConstants.APPSETTING_CONNECTION_STRING);
            DatabaseName = configuration.GetMongoDbDatabase(
                LoggingMongoDbConstants.APPSETTINGS_DATABASE);
            CollectionName = LoggingMongoDbConstants.GetCollectionName(typeof(TDomain).Name);
        }
    }
}