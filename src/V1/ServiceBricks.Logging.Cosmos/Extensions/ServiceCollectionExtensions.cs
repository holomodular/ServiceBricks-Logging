using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging Cosmos module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging Cosmos module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingCosmos(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingCosmosModule), new LoggingCosmosModule());

            // AI: Add the parent module
            // AI: If the primary keys of the Cosmos models do not match the EFC module, we can't use EFC rules, so skip EFC and call start on the core module instead.
            services.AddServiceBricksLogging(configuration); // Skip EFC

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<LoggingCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(
                LoggingCosmosConstants.APPSETTING_CONNECTION_STRING);
            string database = configuration.GetCosmosDatabase(
                LoggingCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);
            services.Configure<DbContextOptions<LoggingCosmosContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingCosmosContext>>(builder.Options);
            services.AddDbContext<LoggingCosmosContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Storage Services for the module for each domain object
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            // AI: Register business rules for the module
            // AI: If the primary keys of the Cosmos models match the EFC module, we can use the EFC rules
            DomainCreateDateRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<LogMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            DomainCreateDateRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<WebRequestMessage>.RegisterRule(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            // AI: If the primary keys of the Cosmos models match the EFC module, we can use the EFC rules
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiService>();
            services.AddScoped<ILogMessageApiService, LogMessageApiService>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiService>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiService>();

            return services;
        }
    }
}