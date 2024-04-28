using System.Reflection;

namespace ServiceBricks.Logging
{
    public class LoggingModule : IModule
    {
        public LoggingModule()
        {
            AdminHtml = string.Empty;
            Name = "Logging Brick";
            Description = @"The Logging Brick is responsible for application logging.";
        }

        public string Name { get; }
        public string Description { get; }
        public string AdminHtml { get; }
        public List<IModule> DependentModules { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
    }
}