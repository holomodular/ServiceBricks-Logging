using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// The module definition for the Logging InMemory module.
    /// </summary>
    public partial class LoggingInMemoryModule : IModule
    {
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

        /// <summary>
        /// The list of dependent modules.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of Automapper assemblies.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of View assemblies.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}