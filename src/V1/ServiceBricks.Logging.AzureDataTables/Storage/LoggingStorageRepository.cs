using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is the storage repository for the Log module.
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public class LoggingStorageRepository<TDomain> : AzureDataTablesStorageRepository<TDomain>
        where TDomain : class, IAzureDataTablesDomainObject<TDomain>, new()
    {
        public LoggingStorageRepository(
            ILoggerFactory logFactory,
            IConfiguration configuration)
            : base(logFactory)
        {
            ConnectionString = configuration.GetAzureDataTablesConnectionString(
                LoggingAzureDataTablesConstants.APPSETTINGS_CONNECTION_STRING);
            TableName = LoggingAzureDataTablesConstants.GetTableName(typeof(TDomain).Name);
            AzureDataTablesOptions = new ServiceQuery.AzureDataTablesOptions()
            {
                // LogMessage/WebRequestMessage will be huge, disable extended operations
                DownloadAllRecordsForAggregate = false,
                DownloadAllRecordsForCount = false,
                DownloadAllRecordsForSort = false,
                DownloadAllRecordsForStringComparison = false
            };
        }
    }
}