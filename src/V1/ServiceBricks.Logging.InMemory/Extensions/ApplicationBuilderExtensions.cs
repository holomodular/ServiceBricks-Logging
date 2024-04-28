using Microsoft.AspNetCore.Builder;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// IApplicationBuilder extensions for the Logging Module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksLoggingInMemory(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            // Start Core Logging
            applicationBuilder.StartServiceBricksLoggingEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}