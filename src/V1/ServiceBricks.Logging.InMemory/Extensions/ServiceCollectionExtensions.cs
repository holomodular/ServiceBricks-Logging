using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging InMemory module to the IServiceCollection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging InMemory module to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingInMemoryModule.Instance);

            // AI: Add module business rules
            LoggingInMemoryModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingInMemoryModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}