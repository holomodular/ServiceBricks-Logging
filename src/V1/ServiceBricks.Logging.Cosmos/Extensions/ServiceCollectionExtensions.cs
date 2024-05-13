using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksLoggingCosmos(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingCosmosModule), new LoggingCosmosModule());

            // Add Core Logging
            services.AddServiceBricksLogging(configuration);

            // Register Database
            var builder = new DbContextOptionsBuilder<LoggingCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(
                LoggingCosmosConstants.APPSETTING_CONNECTION_STRING);
            string database = configuration.GetCosmosDatabase(
                LoggingCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);
            services.Configure<DbContextOptions<LoggingCosmosContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingCosmosContext>>(builder.Options);
            services.AddDbContext<LoggingCosmosContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Register Business rules
            DomainCreateDateRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            DomainCreateDateRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            // Storage Services
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            // API Services
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiService>();
            services.AddScoped<ILogMessageApiService, LogMessageApiService>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiService>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiService>();

            return services;
        }
    }
}