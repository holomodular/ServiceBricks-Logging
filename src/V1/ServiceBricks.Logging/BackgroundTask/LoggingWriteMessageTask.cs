using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a background task that writes log messages to the database.
    /// </summary>
    public static partial class LoggingWriteMessageTask
    {
        public static void QueueWork(this ITaskQueue backgroundTaskQueue)
        {
            backgroundTaskQueue.Queue(new Detail());
        }

        public class Detail : ITaskDetail<Detail, Worker>
        {
            public Detail()
            {
            }
        }

        public class Worker : ITaskWork<Detail, Worker>
        {
            private readonly ILogger<Worker> _logger;
            private readonly ILogMessageApiService _logMessageApiService;

            public Worker(
                ILogger<Worker> logger,
                ILogMessageApiService logMessageApiService)
            {
                _logger = logger;
                _logMessageApiService = logMessageApiService;
            }

            public async Task DoWork(Detail detail, CancellationToken cancellationToken)
            {
                while (CustomLogger.MessageQueue.Count > 0)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    CustomLoggerMessage msg = null;
                    if (CustomLogger.MessageQueue.TryPeek(out msg))
                    {
                        try
                        {
                            var logMsg = new LogMessageDto()
                            {
                                Application = msg.Application,
                                Category = msg.Category,
                                CreateDate = msg.DateTime,
                                Exception = msg.Exception?.ToString(),
                                Level = msg.LogLevel.ToString(),
                                Message = msg.Message,
                                Server = msg.Server,
                                UserStorageKey = msg.UserStorageKey,
                                Path = msg.Path,
                                Properties = msg.Properties != null ? JsonConvert.SerializeObject(msg.Properties) : null
                            };

                            // Create record
                            var respCreate = await _logMessageApiService.CreateAsync(logMsg);

                            if (respCreate.Success)
                                CustomLogger.MessageQueue.TryDequeue(out msg);
                            else
                                return; //Something bad happened, maybe database down, retry next cycle
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