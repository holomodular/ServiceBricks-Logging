using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// Logging Cosmos Module.
    /// </summary>
    public partial class LoggingCosmosModule : IModule
    {
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

        /// <summary>
        /// The list of dependent modules.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of assemblies that contain Automapper profiles.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of view assemblies.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}