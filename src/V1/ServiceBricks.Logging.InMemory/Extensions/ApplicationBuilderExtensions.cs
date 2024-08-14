using Microsoft.AspNetCore.Builder;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// Extensions to start the ServiceBricks Logging InMemory Module
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to check if the module has been started
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Logging InMemory Module
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksLoggingInMemory(this IApplicationBuilder applicationBuilder)
        {
            // AI: Flag the module as started
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksLoggingEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}