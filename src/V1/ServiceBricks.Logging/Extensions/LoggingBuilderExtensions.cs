using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// ILoggerBuilder extensions for the Log module.
    /// </summary>
    public static class ILoggingBuilderExtensions
    {
        public static ILoggingBuilder AddServiceBricksLogging(
            this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());

            return builder;
        }
    }
}