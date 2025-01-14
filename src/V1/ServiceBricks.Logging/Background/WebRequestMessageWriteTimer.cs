using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a timer that will execute the process to write webrequestmessages to the database.
    /// Do not seal the class to allow for overriding values.
    /// </summary>
    public partial class WebRequestMessageWriteTimer : TaskTimerHostedService<WebRequestMessageWriteTask.Detail, WebRequestMessageWriteTask.Worker>
    {
        /// <summary>
        /// Constructor for the LoggingWriteMessageTimer.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="logger"></param>
        public WebRequestMessageWriteTimer(
            IServiceProvider serviceProvider,
            ILoggerFactory logger) : base(serviceProvider, logger)
        {
        }

        /// <summary>
        /// The interval at which the timer will tick.
        /// </summary>
        public override TimeSpan TimerTickInterval
        {
            get { return TimeSpan.FromSeconds(1); }
        }

        /// <summary>
        /// The initial delay before the timer will tick.
        /// </summary>
        public override TimeSpan TimerDueTime
        {
            get { return TimeSpan.FromSeconds(1); }
        }

        /// <summary>
        /// The task detail for the timer that will be executed.
        /// </summary>
        public override ITaskDetail<WebRequestMessageWriteTask.Detail, WebRequestMessageWriteTask.Worker> TaskDetail
        {
            get { return new WebRequestMessageWriteTask.Detail(); }
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
                WebRequestMessageLogger.MessageQueue.Count > 0;
        }
    }
}