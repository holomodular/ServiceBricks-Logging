using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net;
using StateProp = ServiceBricks.Logging.LoggingConstants.MiddlewareStateProperty;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This will put a log message onto a shared queue.
    /// A background task will write the messages to the database.
    /// </summary>
    public sealed class CustomLogger : ILogger
    {
        // Queue to hold all log messages
        public static ConcurrentQueue<CustomLoggerMessage> MessageQueue =
            new ConcurrentQueue<CustomLoggerMessage>();

        private readonly string _categoryName;
        private readonly string _applicationName;
        private readonly IExternalScopeProvider _externalScopeProvider;

        public CustomLogger(
            string categoryName,
            string applicationName,
            IExternalScopeProvider externalScopeProvider)
        {
            _categoryName = categoryName;
            _applicationName = applicationName;
            _externalScopeProvider = externalScopeProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null || _externalScopeProvider == null)
                return null;
            return _externalScopeProvider.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

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

            var msg = new CustomLoggerMessage()
            {
                Application = _applicationName,
                DateTime = DateTime.UtcNow,
                Category = _categoryName,
                EventId = eventId,
                Exception = exception,
                LogLevel = logLevel,
                Message = formatter != null ? formatter(state, exception) : string.Empty,
                Properties = Enumerable.Empty<KeyValuePair<string, object>>(),
                Server = Dns.GetHostName(),
            };

            // Get properties added by CustomLoggerMiddleware
            if (_externalScopeProvider != null)
            {
                _externalScopeProvider.ForEachScope<object>((scope, callback) =>
                {
                    if (scope == null)
                        return;
                    if (scope is IEnumerable<KeyValuePair<string, object>> scopeProperties)
                        msg.Properties = msg.Properties.Concat(scopeProperties);
                }, null);

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
                        if (foundCount == 3) //found all we are after
                            break;
                    }
                }
            }

            // Add message to queue
            MessageQueue.Enqueue(msg);
        }
    }
}