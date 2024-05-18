using System.Reflection;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    public class LoggingEntityFrameworkCoreModule : IModule
    {
        public LoggingEntityFrameworkCoreModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingEntityFrameworkCoreModule).Assembly
            };
        }

        public List<IModule> DependentModules { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
    }
}