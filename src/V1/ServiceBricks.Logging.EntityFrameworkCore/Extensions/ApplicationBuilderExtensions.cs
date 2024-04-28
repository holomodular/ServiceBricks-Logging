using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// IApplicationBuilder extensions for the Logging Module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksLoggingEntityFrameworkCore(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            // Start Core Logging
            applicationBuilder.StartServiceBricksLogging();

            return applicationBuilder;
        }
    }
}