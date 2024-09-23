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
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingModule), new LoggingModule());

            // AI: Add any custom requirements for the module
            services.AddLogging();

            // AI: Add hosted services for the module
            services.AddHostedService<CustomLoggerWriteMessageTimer>();

            // AI: Add workers for tasks in the module
            services.AddScoped<CustomLoggerWriteMessageTask.Worker>();

            // AI: Configure all options for the module
            services.Configure<WebRequestMessageOptions>(configuration.GetSection(LoggingConstants.APPSETTING_WEBREQUESTMESSAGE));

            // AI: Add API Controllers for each DTO in the module
            services.AddScoped<ILogMessageApiController, LogMessageApiController>();
            services.AddScoped<IWebRequestMessageApiController, WebRequestMessageApiController>();

            // AI: Add any miscellaneous services for the module
            services.AddTransient<WebRequestMessageMiddleware>();

            // AI: Register business rules for the module

            // AI: Register servicebus subscriptions for the module
            using (var serviceScope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceBus = serviceScope.ServiceProvider.GetRequiredService<IServiceBus>();
                CreateApplicationLogRule.Register(serviceBus);
            }

            return services;
        }

        /// <summary>
        /// Add the ServiceBricks Logging client to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingClient(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the module for each DTO
            services.AddScoped<ILogMessageApiClient, LogMessageApiClient>();

            return services;
        }
    }
}