using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging MongoDb module to the IServiceCollection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging MongoDb module to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the parent module
            services.AddServiceBricksLogging(configuration);

            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.Register(LoggingMongoDbModule.Instance);

            // AI: Add module business rules
            LoggingMongoDbModuleAddRule.Register(BusinessRuleRegistry.Instance);
            ModuleSetStartedRule<LoggingMongoDbModule>.Register(BusinessRuleRegistry.Instance);

            return services;
        }
    }
}