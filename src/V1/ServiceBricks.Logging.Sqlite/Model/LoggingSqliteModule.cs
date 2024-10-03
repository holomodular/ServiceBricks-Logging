using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.Sqlite
{
    /// <summary>
    /// The module definition for the ServiceBricks.Logging.Sqlite module.
    /// </summary>
    public partial class LoggingSqliteModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance.
        /// </summary>
        public static LoggingSqliteModule Instance = new LoggingSqliteModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingSqliteModule()
        {
            DependentModules = new List<IModule>()
            {
                new LoggingEntityFrameworkCoreModule()
            };
        }
    }
}