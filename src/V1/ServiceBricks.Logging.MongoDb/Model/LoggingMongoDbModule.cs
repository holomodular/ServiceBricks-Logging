using System.Reflection;

namespace ServiceBricks.Logging.MongoDb
{
    public class LoggingMongoDbModule : IModule
    {
        public LoggingMongoDbModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingMongoDbModule).Assembly
            };
        }

        public List<IModule> DependentModules { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
    }
}