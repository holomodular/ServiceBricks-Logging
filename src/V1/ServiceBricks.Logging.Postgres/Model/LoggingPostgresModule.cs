using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.Postgres
{
    public class LoggingPostgresModule : IModule
    {
        public LoggingPostgresModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingPostgresModule).Assembly
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