using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// Extensions to start the ServiceBricks Logging EntityFrameworkCore module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to determine if the module has started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Logging EntityFrameworkCore module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksLoggingEntityFrameworkCore(this IApplicationBuilder applicationBuilder)
        {
            // AI: Set the module started flag when complete
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksLogging();

            return applicationBuilder;
        }
    }
}