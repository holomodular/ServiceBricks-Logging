using System.Collections.Concurrent;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// THis is used to store messages in memory to be pulled off later
    /// </summary>
    public sealed class WebRequestMessageLogger
    {
        /// <summary>
        /// This is the queue where log messages are stored.
        /// </summary>
        public static ConcurrentQueue<WebRequestMessageDto> MessageQueue =
            new ConcurrentQueue<WebRequestMessageDto>();
    }
}