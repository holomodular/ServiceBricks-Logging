using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// ILoggerBuilder extensions for the ServiceBricks Logging module.
    /// </summary>
    public static partial class ILoggingBuilderExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging module to the ILoggerBuilder (typically in Program.cs for system startup).
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddServiceBricksLogging(
            this ILoggingBuilder builder)
        {
            // AI: This injects the CustomLoggerProvider into the ILoggerProvider collection.
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());

            return builder;
        }
    }
}