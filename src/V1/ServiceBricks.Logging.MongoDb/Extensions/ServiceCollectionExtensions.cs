using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksLoggingMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingMongoDbModule), new LoggingMongoDbModule());

            // Add Core Logging
            services.AddServiceBricksLogging(configuration);

            // Storage Services
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            // API Services
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiService>();
            services.AddScoped<ILogMessageApiService, LogMessageApiService>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiService>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiService>();

            // Business Rules
            DomainCreateDateRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            DomainCreateDateRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return services;
        }
    }
}