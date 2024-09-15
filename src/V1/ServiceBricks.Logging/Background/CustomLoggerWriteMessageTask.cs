using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a background task that writes log messages from the CusomLogger.MessageQueue to the database.
    /// </summary>
    public static class CustomLoggerWriteMessageTask
    {
        /// <summary>
        /// Queue the work to the background task queue.
        /// </summary>
        /// <param name="backgroundTaskQueue"></param>
        public static void QueueWork(this ITaskQueue backgroundTaskQueue)
        {
            backgroundTaskQueue.Queue(new Detail());
        }

        /// <summary>
        /// The detail class provides any additional information needed to perform the work.
        /// In this case, no additional information is needed as it will pull messages from the CustomLogger.MessageQueue.
        /// </summary>
        public class Detail : ITaskDetail<Detail, Worker>
        {
        }

        /// <summary>
        /// The worker class performs the work detail. It should also be registered as a scoped service in the DI container in ServiceCollectionExtensions.
        /// </summary>
        public class Worker : ITaskWork<Detail, Worker>
        {
            private readonly ILogger<Worker> _logger;
            private readonly ILogMessageApiService _logMessageApiService;

            /// <summary>
            /// Constructor for the worker class.
            /// </summary>
            /// <param name="logger"></param>
            /// <param name="logMessageApiService"></param>
            public Worker(
                ILoggerFactory loggerFactory,
                ILogMessageApiService logMessageApiService)
            {
                _logger = loggerFactory.CreateLogger<Worker>();
                _logMessageApiService = logMessageApiService;
            }

            /// <summary>
            /// Perform the work detail.
            /// </summary>
            /// <param name="detail"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task DoWork(Detail detail, CancellationToken cancellationToken)
            {
                // AI: Iterate through the message queue and write to the database
                while (CustomLogger.MessageQueue.Count > 0)
                {
                    // AI: While writing messages, check if cancellation is requested and exit
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    // AI: Peek at the message from the queue and try to process it first
                    CustomLoggerMessage msg = null;
                    if (CustomLogger.MessageQueue.TryPeek(out msg))
                    {
                        try
                        {
                            // AI: Create a LogMessageDto from the CustomLoggerMessage
                            var logMsg = new LogMessageDto()
                            {
                                Application = msg.Application,
                                Category = msg.Category,
                                CreateDate = msg.CreateDate,
                                Exception = msg.Exception?.ToString(),
                                Level = msg.LogLevel.ToString(),
                                Message = msg.Message,
                                Server = msg.Server,
                                UserStorageKey = msg.UserStorageKey,
                                Path = msg.Path,
                                Properties = msg.Properties != null ? JsonConvert.SerializeObject(msg.Properties) : null
                            };

                            // AI: Create record
                            var respCreate = await _logMessageApiService.CreateAsync(logMsg);

                            // AI: If successful, dequeue the message
                            if (respCreate.Success)
                                CustomLogger.MessageQueue.TryDequeue(out msg);
                            else
                            {
                                // AI: There is an error, maybe database down. It will be retried the next time the next task cycle runs.
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);
                        }
                    }
                }
            }
        }
    }
}