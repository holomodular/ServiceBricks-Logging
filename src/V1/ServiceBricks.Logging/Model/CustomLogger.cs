using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net;
using StateProp = ServiceBricks.Logging.LoggingConstants.MiddlewareStateProperty;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This class is a custom logger that logs messages to an in-memory queue.
    /// </summary>
    public sealed class CustomLogger : ILogger
    {
        /// <summary>
        /// This is the queue where log messages are stored.
        /// </summary>
        public static ConcurrentQueue<CustomLoggerMessage> MessageQueue =
            new ConcurrentQueue<CustomLoggerMessage>();

        private readonly string _categoryName;
        private readonly string _applicationName;
        private readonly IExternalScopeProvider _externalScopeProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="applicationName"></param>
        /// <param name="externalScopeProvider"></param>
        public CustomLogger(
            string categoryName,
            string applicationName,
            IExternalScopeProvider externalScopeProvider)
        {
            _categoryName = categoryName;
            _applicationName = applicationName;
            _externalScopeProvider = externalScopeProvider;
        }

        /// <summary>
        /// Begin scope
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null || _externalScopeProvider == null)
                return null;
            return _externalScopeProvider.Push(state);
        }

        /// <summary>
        /// Is enabled
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Log a message
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            Exception? exception,
            Func<TState, Exception?, string> formatter)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (!IsEnabled(logLevel))
                return;

            // AI: Create a new CustomLoggerMessage
            var msg = new CustomLoggerMessage()
            {
                Application = _applicationName,
                CreateDate = DateTimeOffset.UtcNow,
                Category = _categoryName,
                EventId = eventId,
                Exception = exception,
                LogLevel = logLevel,
                Message = formatter != null ? formatter(state, exception) : string.Empty,
                Properties = Enumerable.Empty<KeyValuePair<string, object>>(),
                Server = Dns.GetHostName(),
            };

            // AI: Get properties added by CustomLoggerMiddleware
            if (_externalScopeProvider != null)
            {
                _externalScopeProvider.ForEachScope<object>((scope, callback) =>
                {
                    if (scope == null)
                        return;
                    if (scope is IEnumerable<KeyValuePair<string, object>> scopeProperties)
                        msg.Properties = msg.Properties.Concat(scopeProperties);
                }, null);

                // AI: Extract properties from state to named properties
                if (msg.Properties != null)
                {
                    int count = 0;
                    int foundCount = 0;
                    foreach (var property in msg.Properties)
                    {
                        count++;
                        if (property.Key == StateProp.USER_STORAGE_KEY)
                        {
                            msg.UserStorageKey = property.Value as string;
                            foundCount++;
                        }
                        if (property.Key == StateProp.PATH)
                        {
                            msg.Path = property.Value as string;
                            foundCount++;
                        }
                        if (property.Key == StateProp.IPADDRESS)
                        {
                            msg.IpAddress = property.Value as string;
                            foundCount++;
                        }

                        // AI: We only need to find 3 properties (listed above), break if we found them all
                        if (foundCount == 3)
                            break;
                    }
                }
            }

            // AI: Finally, add the message to queue
            MessageQueue.Enqueue(msg);
        }
    }
}