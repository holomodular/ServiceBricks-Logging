using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.SqlServer
{
    public class LoggingSqlServerModule : IModule
    {
        public LoggingSqlServerModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingSqlServerModule).Assembly
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