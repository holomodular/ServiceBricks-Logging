using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.MongoDb;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is the storage repository for the ServiceBricks Logging MongoDb module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public partial class LoggingStorageRepository<TDomain> : MongoDbStorageRepository<TDomain>
        where TDomain : class, IMongoDbDomainObject<TDomain>, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logFactory"></param>
        /// <param name="configuration"></param>
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