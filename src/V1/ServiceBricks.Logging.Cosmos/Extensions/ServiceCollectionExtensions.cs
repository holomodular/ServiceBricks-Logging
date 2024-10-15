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

            // AI: Remove the EFC rule since we are using a different models
            LoggingEntityFrameworkCoreModuleAddRule.UnRegister(BusinessRuleRegistry.Instance);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingCosmosModule.Instance);

            // AI: Add module business rules
            LoggingCosmosModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingCosmosModule>.Register(BusinessRuleRegistry.Instance);
            EntityFrameworkCoreDatabaseEnsureCreatedRule<LoggingModule, LoggingCosmosContext>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}