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
        /// Add the ServiceBricks Logging client to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingClient(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the module for each DTO
            services.AddScoped<IApiClient<LogMessageDto>, LogMessageApiClient>();
            services.AddScoped<ILogMessageApiClient, LogMessageApiClient>();

            services.AddScoped<IApiClient<WebRequestMessageDto>, WebRequestMessageApiClient>();
            services.AddScoped<IWebRequestMessageApiClient, WebRequestMessageApiClient>();

            return services;
        }

        /// <summary>
        /// Add the ServiceBricks Logging clients to the service collection for the API Service references
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingClientForService(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add clients for the API Services
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiClient>();
            services.AddScoped<ILogMessageApiService, LogMessageApiClient>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiClient>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiClient>();

            return services;
        }

    }
}