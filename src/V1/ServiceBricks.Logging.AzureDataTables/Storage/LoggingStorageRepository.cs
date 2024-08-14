using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is the storage repository for the ServiceBricks Logging module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public partial class LoggingStorageRepository<TDomain> : AzureDataTablesStorageRepository<TDomain>
        where TDomain : class, IAzureDataTablesDomainObject<TDomain>, new()
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
            ConnectionString = configuration.GetAzureDataTablesConnectionString(
                LoggingAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);
            TableName = LoggingAzureDataTablesConstants.GetTableName(typeof(TDomain).Name);
            AzureDataTablesOptions = new ServiceQuery.AzureDataTablesOptions()
            {
                // AI: LogMessage and WebRequestMessages will be huge, disable all extended query operations for the module
                DownloadAllRecordsForAggregate = false,
                DownloadAllRecordsForCount = false,
                DownloadAllRecordsForDistinct = false,
                DownloadAllRecordsForSort = false,
                DownloadAllRecordsForStringComparison = false,
            };
        }
    }
}