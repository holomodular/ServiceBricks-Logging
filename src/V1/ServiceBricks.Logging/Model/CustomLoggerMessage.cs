using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is an application log message with state information.
    /// </summary>
    public class CustomLoggerMessage
    {
        /// <summary>
        /// The date and time of the log message.
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The application name.
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// The server name.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The category of the log message.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The request path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The severity level of the log message.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// The EventID of the log message.
        /// </summary>
        public EventId EventId { get; set; }

        /// <summary>
        /// The exception of the log message.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// The message of the log message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The requesting IP address of the log message.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The user storage key of the log message.
        /// </summary>
        public string UserStorageKey { get; set; }

        /// <summary>
        /// The dynamic list of properties associated with the log message.
        /// </summary>
        public IEnumerable<KeyValuePair<string, object>> Properties { get; set; }
    }
}