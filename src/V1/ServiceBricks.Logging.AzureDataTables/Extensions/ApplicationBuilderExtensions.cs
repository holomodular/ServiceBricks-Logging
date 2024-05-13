using Azure.Data.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// IApplicationBuilder extensions for the Log Module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksLoggingAzureDataTables(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetAzureDataTablesConnectionString(
                    LoggingAzureDataTablesConstants.APPSETTING_CONNECTION_STRING);

                // Create each table if not exists
                TableClient tableClient = new TableClient(
                    connectionString,
                    LoggingAzureDataTablesConstants.GetTableName(nameof(LogMessage)));
                tableClient.CreateIfNotExists();

                tableClient = new TableClient(
                    connectionString,
                    LoggingAzureDataTablesConstants.GetTableName(nameof(WebRequestMessage)));
                tableClient.CreateIfNotExists();
            }
            ModuleStarted = true;

            // Start Logging Core
            applicationBuilder.StartServiceBricksLogging();

            return applicationBuilder;
        }
    }
}