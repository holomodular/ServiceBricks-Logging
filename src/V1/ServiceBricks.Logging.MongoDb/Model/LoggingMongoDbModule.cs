using System.Reflection;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is the logging MongoDb module.
    /// </summary>
    public partial class LoggingMongoDbModule : IModule
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LoggingMongoDbModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingMongoDbModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new LoggingModule()
            };
        }

        /// <summary>
        /// The list of dependent modules.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of automapper assemblies.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of view assemblies.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}