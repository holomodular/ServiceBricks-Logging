using System.Reflection;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    public class LoggingEntityFrameworkCoreModule : IModule
    {
        public LoggingEntityFrameworkCoreModule()
        {
            AdminHtml = string.Empty;
            Name = "Logging EntityFrameworkCore Brick";
            Description = @"The Logging EntityFrameworkCore Brick implements the Entity Framework Core provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingEntityFrameworkCoreModule).Assembly
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