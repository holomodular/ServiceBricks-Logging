using System.Reflection;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is the module definition for the ServiceBricks Logging Azure Data Tables module.
    /// </summary>
    public partial class LoggingAzureDataTablesModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static LoggingAzureDataTablesModule Instance = new LoggingAzureDataTablesModule();

        /// <summary>
        /// Constructor
        /// </summary>
        public LoggingAzureDataTablesModule()
        {
            DependentModules = new List<IModule>()
            {
                new LoggingModule()
            };
        }
    }
}