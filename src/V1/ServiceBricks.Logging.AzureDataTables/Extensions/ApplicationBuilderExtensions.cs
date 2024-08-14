using Azure.Data.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// Extensions for starting the ServiceBricks Logging Azure Data Tables module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Logging Azure Data Tables module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksLoggingAzureDataTables(this IApplicationBuilder applicationBuilder)
        {
            // AI: Get the connection string
            var configuration = applicationBuilder.ApplicationServices.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetAzureDataTablesConnectionString(
                LoggingAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);

            // AI: Create each table in the module
            TableClient tableClient = new TableClient(
                connectionString,
                LoggingAzureDataTablesConstants.GetTableName(nameof(LogMessage)));
            tableClient.CreateIfNotExists();

            tableClient = new TableClient(
                connectionString,
                LoggingAzureDataTablesConstants.GetTableName(nameof(WebRequestMessage)));
            tableClient.CreateIfNotExists();

            // AI: Set the module started flag
            ModuleStarted = true;

            // AI: Start parent module
            applicationBuilder.StartServiceBricksLogging();

            return applicationBuilder;
        }
    }
}