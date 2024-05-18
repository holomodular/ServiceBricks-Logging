using System.Reflection;

namespace ServiceBricks.Logging.AzureDataTables
{
    public class LoggingAzureDataTablesModule : IModule
    {
        public LoggingAzureDataTablesModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingAzureDataTablesModule).Assembly
            };
        }

        public List<IModule> DependentModules { get; }
        public List<Assembly> AutomapperAssemblies { get; }
        public List<Assembly> ViewAssemblies { get; }
    }
}