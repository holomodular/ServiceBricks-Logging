using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging EntityFrameworkCore module to the IServiceCollection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging EntityFrameworkCore module to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module
            services.AddServiceBricksLogging(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingEntityFrameworkCoreModule.Instance);

            // AI: Add module business rules
            LoggingEntityFrameworkCoreModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingEntityFrameworkCoreModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}