using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a timer that will execute the LoggingWriteMessageTask which will write log messages to the database.
    /// Do not seal the class to allow for overriding values.
    /// </summary>
    public class CustomLoggerWriteMessageTimer : TaskTimerHostedService<CustomLoggerWriteMessageTask.Detail, CustomLoggerWriteMessageTask.Worker>
    {
        /// <summary>
        /// Constructor for the LoggingWriteMessageTimer.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public CustomLoggerWriteMessageTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger) : base(serviceProvider, logger)
        {
            TimerTickInterval = TimeSpan.FromSeconds(1);
            TimerDueTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// The task detail for the timer that will be executed.
        /// </summary>
        public override ITaskDetail<CustomLoggerWriteMessageTask.Detail, CustomLoggerWriteMessageTask.Worker> TaskDetail
        {
            get { return new CustomLoggerWriteMessageTask.Detail(); }
        }

        /// <summary>
        /// Determine if the timer should process the run.
        /// </summary>
        /// <returns></returns>
        public override bool TimerTickShouldProcessRun()
        {
            // AI: Check if the module has started, the timer is not currently running, and there are messages in the queue
            return LoggingModule.Instance.Started &&
                !IsCurrentlyRunning &&
                CustomLogger.MessageQueue.Count > 0;
        }
    }
}