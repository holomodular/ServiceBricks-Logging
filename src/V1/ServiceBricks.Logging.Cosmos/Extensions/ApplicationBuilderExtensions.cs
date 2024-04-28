using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// IApplicationBuilder extensions for the Logging Module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksLoggingCosmos(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            using (var scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LoggingCosmosContext>();
                context.Database.EnsureCreated();
            }

            // Start Core Logging
            applicationBuilder.StartServiceBricksLogging();

            return applicationBuilder;
        }
    }
}