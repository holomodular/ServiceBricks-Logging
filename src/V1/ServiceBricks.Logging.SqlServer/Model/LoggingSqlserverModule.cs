using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.SqlServer
{
    public class LoggingSqlServerModule : IModule
    {
        public LoggingSqlServerModule()
        {
            AdminHtml = string.Empty;
            Name = "Logging InMemory Brick";
            Description = @"The Logging InMemory Brick implements the Azure Data Tables provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingSqlServerModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new LoggingEntityFrameworkCoreModule()
            };
        }

        public string Name { get; }
        public string Description { get; }
        public string AdminHtml { get; }

        public List<IModule> DependentModules { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
    }
}