using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.SqlServer
{
    /// <summary>
    /// The module definition for the ServiceBricks.Logging.SqlServer module.
    /// </summary>
    public partial class LoggingSqlServerModule : IModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingSqlServerModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingSqlServerModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new LoggingEntityFrameworkCoreModule()
            };
        }

        /// <summary>
        /// The list of dependent modules.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of Automapper assemblies.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of view assemblies.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}