using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// Extension methods for starting the ServiceBricks Logging module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to indicate if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Logging module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksLogging(this IApplicationBuilder applicationBuilder)
        {
            // AI: Set the module started flag
            ModuleStarted = true;

            return applicationBuilder;
        }
    }
}