using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.Cosmos
{
    public class LoggingCosmosModule : IModule
    {
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

        public List<IModule> DependentModules { get; }

        public List<Assembly> AutomapperAssemblies { get; }

        public List<Assembly> ViewAssemblies { get; }
    }
}