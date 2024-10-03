using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.SqlServer
{
    /// <summary>
    /// The module definition for the ServiceBricks.Logging.SqlServer module.
    /// </summary>
    public partial class LoggingSqlServerModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static LoggingSqlServerModule Instance = new LoggingSqlServerModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingSqlServerModule()
        {
            DependentModules = new List<IModule>()
            {
                new LoggingEntityFrameworkCoreModule()
            };
        }
    }
}