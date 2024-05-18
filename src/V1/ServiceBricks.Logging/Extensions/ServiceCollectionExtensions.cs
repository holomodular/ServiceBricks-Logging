using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the Logging Brick.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLogging(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingModule), new LoggingModule());

            services.AddLogging();

            // Background Tasks
            services.AddHostedService<LoggingWriteMessageTimer>();
            services.AddScoped<LoggingWriteMessageTask.Worker>();

            // Options
            services.Configure<WebRequestMessageOptions>(configuration.GetSection(LoggingConstants.APPSETTING_WEBREQUESTMESSAGE));

            // API Controllers
            services.AddScoped<ILogMessageApiController, LogMessageApiController>();
            services.AddScoped<IWebRequestMessageApiController, WebRequestMessageApiController>();

            // Misc
            services.AddTransient<WebRequestMessageMiddleware>();

            // ServiceBus Rules
            using (var serviceScope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var serviceBus = serviceScope.ServiceProvider.GetRequiredService<IServiceBus>();
                CreateApplicationLogRule.RegisterServiceBus(serviceBus);
            }

            return services;
        }

        public static IServiceCollection AddServiceBricksLoggingClient(this IServiceCollection services, IConfiguration configuration)
        {
            // Clients
            services.AddScoped<ILogMessageApiClient, LogMessageApiClient>();

            return services;
        }
    }
}