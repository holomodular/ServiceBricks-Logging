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
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingAzureDataTablesModule), new LoggingAzureDataTablesModule());

            // AI: Add parent module
            services.AddServiceBricksLogging(configuration);

            // AI: Configure all options for the module

            // AI: Add storage services for the module. Each domain object should have its own storage repository
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiService>();
            services.AddScoped<ILogMessageApiService, LogMessageApiService>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiService>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiService>();

            // AI: Register business rules for the module
            DomainCreateDateRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            LogMessageCreateRule.RegisterRule(BusinessRuleRegistry.Instance);
            LogMessageQueryRule.RegisterRule(BusinessRuleRegistry.Instance);

            DomainCreateDateRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            WebRequestMessageCreateRule.RegisterRule(BusinessRuleRegistry.Instance);
            WebRequestMessageQueryRule.RegisterRule(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}