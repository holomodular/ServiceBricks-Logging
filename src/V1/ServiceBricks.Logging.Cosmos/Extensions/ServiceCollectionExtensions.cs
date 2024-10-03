using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
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
            // AI: Add the parent module
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingCosmosModule.Instance);

            // AI: Add module business rules
            LoggingCosmosModuleAddRule.Register(BusinessRuleRegistry.Instance);
            EntityFrameworkCoreDatabaseEnsureCreatedRule<LoggingCosmosModule, LoggingCosmosContext>.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingCosmosModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}