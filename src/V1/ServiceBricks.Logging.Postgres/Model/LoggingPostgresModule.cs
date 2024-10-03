using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.Postgres
{
    /// <summary>
    /// The module for the ServiceBricks Logging Postgres module.
    /// </summary>
    public partial class LoggingPostgresModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static LoggingPostgresModule Instance = new LoggingPostgresModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingPostgresModule()
        {
            DependentModules = new List<IModule>()
            {
                new LoggingEntityFrameworkCoreModule()
            };
        }
    }
}