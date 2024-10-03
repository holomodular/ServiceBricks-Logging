using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.Postgres;

namespace ServiceBricks.Logging.Postgres
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging Postgres module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging Postgres module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingPostgresModule.Instance);

            // AI: Add module business rules
            LoggingPostgresModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingPostgresModule>.Register(BusinessRuleRegistry.Instance);
            PostgresDatabaseMigrationRule<LoggingPostgresModule, LoggingPostgresContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}