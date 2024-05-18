using System.Reflection;

namespace ServiceBricks.Logging
{
    public class LoggingModule : IModule
    {
        public LoggingModule()
        {
            AutomapperAssemblies = new List<Assembly>()
         {
             typeof(LoggingModule).Assembly
         };
        }

        public List<IModule> DependentModules { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
    }
}