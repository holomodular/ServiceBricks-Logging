using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.Sqlite
{
    public class LoggingSqliteModule : IModule
    {
        public LoggingSqliteModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingSqliteModule).Assembly
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