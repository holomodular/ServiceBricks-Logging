namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is the module definition for the ServiceBricks Logging module.
    /// </summary>
    public partial class LoggingModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static LoggingModule Instance = new LoggingModule();

        /// <summary>
        /// Constructor
        /// </summary>
        public LoggingModule()
        {
            DataTransferObjects = new List<Type>()
            {
                typeof(LogMessageDto),
                typeof(WebRequestMessageDto)
            };
        }
    }
}