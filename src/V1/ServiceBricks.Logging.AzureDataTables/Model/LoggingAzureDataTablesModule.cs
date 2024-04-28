using System.Reflection;

namespace ServiceBricks.Logging.AzureDataTables
{
    public class LoggingAzureDataTablesModule : IModule
    {
        public LoggingAzureDataTablesModule()
        {
            AdminHtml = string.Empty;
            Name = "Logging AzureDataTables Brick";
            Description = @"The Logging AzureDataTables Brick implements the Azure Data Tables provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingAzureDataTablesModule).Assembly
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