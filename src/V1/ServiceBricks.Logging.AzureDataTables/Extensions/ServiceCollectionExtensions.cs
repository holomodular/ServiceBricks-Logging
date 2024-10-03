using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging Azure Data Tables module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging Azure Data Tables module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingAzureDataTables(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksLogging(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingAzureDataTablesModule.Instance);

            // AI: Add module business rules
            LoggingAzureDataTablesModuleAddRule.Register(BusinessRuleRegistry.Instance);
            LoggingAzureDataTablesModuleStartRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingAzureDataTablesModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}