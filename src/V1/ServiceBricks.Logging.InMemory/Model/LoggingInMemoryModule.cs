using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.InMemory
{
    public class LoggingInMemoryModule : IModule
    {
        public LoggingInMemoryModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingInMemoryModule).Assembly
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