using ServiceBricks.Logging.EntityFrameworkCore;
using System.Reflection;

namespace ServiceBricks.Logging.InMemory
{
    public class LoggingInMemoryModule : IModule
    {
        public LoggingInMemoryModule()
        {
            AdminHtml = string.Empty;
            Name = "Logging InMemory Brick";
            Description = @"The Logging InMemory Brick implements the Azure Data Tables provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingInMemoryModule).Assembly
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