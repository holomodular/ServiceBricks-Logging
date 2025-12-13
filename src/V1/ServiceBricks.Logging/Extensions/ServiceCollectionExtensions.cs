using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// Extensions for adding the ServiceBricks Logging module.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging module to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLogging(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingModule.Instance);

            // AI: Add module business rules
            LoggingModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}