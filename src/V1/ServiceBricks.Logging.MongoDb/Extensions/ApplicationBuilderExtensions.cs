using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// IApplicationBuilder extensions for the Log Module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        public static IApplicationBuilder StartServiceBricksLoggingMongoDb(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;

            // Start core logging
            applicationBuilder.StartServiceBricksLogging();

            return applicationBuilder;
        }
    }
}