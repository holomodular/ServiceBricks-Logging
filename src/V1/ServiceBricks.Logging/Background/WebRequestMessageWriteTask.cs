using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a background task that writes log messages from the CusomLogger.MessageQueue to the database.
    /// </summary>
    public static class WebRequestMessageWriteTask
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
        /// In this case, no additional information is needed as it will pull messages from the WebRequestMessageLogger.MessageQueue.
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
            private readonly IWebRequestMessageApiService _webRequestMessageApiService;

            /// <summary>
            /// Constructor for the worker class.
            /// </summary>
            /// <param name="logger"></param>
            /// <param name="webRequestMessageApiService"></param>
            public Worker(
                ILoggerFactory loggerFactory,
                IWebRequestMessageApiService webRequestMessageApiService)
            {
                _logger = loggerFactory.CreateLogger<Worker>();
                _webRequestMessageApiService = webRequestMessageApiService;
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
                while (WebRequestMessageLogger.MessageQueue.Count > 0)
                {
                    // AI: While writing messages, check if cancellation is requested and exit
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    // AI: Peek at the message from the queue and try to process it first
                    WebRequestMessageDto msg = null;
                    if (WebRequestMessageLogger.MessageQueue.TryPeek(out msg))
                    {
                        try
                        {
                            // AI: Create record
                            var respCreate = await _webRequestMessageApiService.CreateAsync(msg);

                            // AI: If successful, dequeue the message
                            if (respCreate.Success)
                                WebRequestMessageLogger.MessageQueue.TryDequeue(out msg);
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