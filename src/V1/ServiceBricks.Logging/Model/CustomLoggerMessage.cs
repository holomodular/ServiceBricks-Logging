using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is an application log message with state information.
    /// </summary>
    public class CustomLoggerMessage
    {
        public DateTime DateTime { get; set; }
        public string Application { get; set; }
        public string Server { get; set; }
        public string Category { get; set; }
        public string Path { get; set; }
        public LogLevel LogLevel { get; set; }
        public EventId EventId { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public string IpAddress { get; set; }
        public string UserStorageKey { get; set; }
        public IEnumerable<KeyValuePair<string, object>> Properties { get; set; }
    }
}