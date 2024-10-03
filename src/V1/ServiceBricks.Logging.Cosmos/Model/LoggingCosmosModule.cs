using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// Logging Cosmos Module.
    /// </summary>
    public partial class LoggingCosmosModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static LoggingCosmosModule Instance = new LoggingCosmosModule();

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingCosmosModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingCosmosModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new LoggingEntityFrameworkCoreModule()
            };
        }
    }
}