using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksLoggingAzureDataTables(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingAzureDataTablesModule), new LoggingAzureDataTablesModule());

            // Add Logging Core
            services.AddServiceBricksLogging(configuration);

            // Storage Services
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            // API Services
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiService>();
            services.AddScoped<ILogMessageApiService, LogMessageApiService>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiService>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiService>();

            // Register Business rules
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