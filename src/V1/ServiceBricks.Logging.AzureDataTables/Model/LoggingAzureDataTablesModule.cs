using System.Reflection;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is the module definition for the ServiceBricks Logging Azure Data Tables module.
    /// </summary>
    public partial class LoggingAzureDataTablesModule : IModule
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LoggingAzureDataTablesModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingAzureDataTablesModule).Assembly
            };
        }

        /// <summary>
        /// The list of dependent modules for the module.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// The list of assemblies that contain AutoMapper profiles for the module.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// The list of assemblies that contain view models for the module.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}