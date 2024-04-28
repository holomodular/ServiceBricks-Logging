using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a timer to execute the background task to write log messages to the database.
    /// </summary>
    public class LoggingWriteMessageTimer : TaskTimerHostedService<LoggingWriteMessageTask.Detail, LoggingWriteMessageTask.Worker>
    {
        public LoggingWriteMessageTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger) : base(serviceProvider, logger)
        {
        }

        public override TimeSpan TimerTickInterval
        {
            get { return TimeSpan.FromSeconds(1); }
        }

        public override TimeSpan TimerDueTime
        {
            get { return TimeSpan.FromSeconds(1); }
        }

        public override ITaskDetail<LoggingWriteMessageTask.Detail, LoggingWriteMessageTask.Worker> TaskDetail
        {
            get { return new LoggingWriteMessageTask.Detail(); }
        }

        public override bool TimerTickShouldProcessRun()
        {
            return ApplicationBuilderExtensions.ModuleStarted &&
                !IsCurrentlyRunning &&
                CustomLogger.MessageQueue.Count > 0;
        }
    }
}