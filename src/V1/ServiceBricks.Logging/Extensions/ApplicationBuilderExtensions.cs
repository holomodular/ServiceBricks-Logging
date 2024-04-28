using Microsoft.AspNetCore.Builder;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// Extensions for the Logging Brick.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the Logging Brick.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksLogging(this IApplicationBuilder applicationBuilder)
        {
            ModuleStarted = true;
            return applicationBuilder;
        }
    }
}