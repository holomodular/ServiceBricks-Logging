using System.Reflection;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// The module definition for the ServiceBricks Logging EntityFrameworkCore module.
    /// </summary>
    public partial class LoggingEntityFrameworkCoreModule : IModule
    {
        /// <summary>
        /// Constructor for the ServiceBricks Logging EntityFrameworkCore module.
        /// </summary>
        public LoggingEntityFrameworkCoreModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingEntityFrameworkCoreModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new LoggingModule()
            };
        }

        /// <summary>
        /// The list of dependent modules for the ServiceBricks Logging EntityFrameworkCore module.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of Automapper assemblies for the ServiceBricks Logging EntityFrameworkCore module.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of view assemblies for the ServiceBricks Logging EntityFrameworkCore module.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}