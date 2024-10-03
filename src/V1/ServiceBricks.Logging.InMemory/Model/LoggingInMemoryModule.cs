using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// The module definition for the Logging InMemory module.
    /// </summary>
    public partial class LoggingInMemoryModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static LoggingInMemoryModule Instance = new LoggingInMemoryModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingInMemoryModule()
        {
            DependentModules = new List<IModule>()
            {
                new LoggingEntityFrameworkCoreModule()
            };
        }
    }
}