using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// Extensions to start the ServiceBricks Logging MongoDb module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Indicates if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Logging MongoDb module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksLoggingMongoDb(this IApplicationBuilder applicationBuilder)
        {
            // AI: Flag the module as started
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksLogging();

            return applicationBuilder;
        }
    }
}