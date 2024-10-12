using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;
using ServiceBricks.Storage.Sqlite;

namespace ServiceBricks.Logging.Sqlite
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging Sqlite module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging Sqlite module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add parent module
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // AI: Add this module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingSqliteModule.Instance);

            // AI: Add module business rules
            LoggingSqliteModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingSqliteModule>.Register(BusinessRuleRegistry.Instance);
            SqliteDatabaseMigrationRule<LoggingModule, LoggingSqliteContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}