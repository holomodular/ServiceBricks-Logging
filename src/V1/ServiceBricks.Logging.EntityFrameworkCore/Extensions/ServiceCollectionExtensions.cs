using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksLoggingEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingEntityFrameworkCoreModule), new LoggingEntityFrameworkCoreModule());

            // Add Core Logging
            services.AddServiceBricksLogging(configuration);

            // API Services
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiService>();
            services.AddScoped<ILogMessageApiService, LogMessageApiService>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiService>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiService>();

            // Register Business rules
            DomainCreateDateRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            DomainCreateDateRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return services;
        }
    }
}