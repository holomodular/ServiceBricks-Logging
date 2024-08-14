using System.Reflection;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is the module definition for the ServiceBricks Logging module.
    /// </summary>
    public partial class LoggingModule : IModule
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LoggingModule()
        {
            AutomapperAssemblies = new List<Assembly>()
             {
                 typeof(LoggingModule).Assembly
             };
        }

        /// <summary>
        /// List of dependent modules.
        /// </summary>
        public List<IModule> DependentModules { get; }

        /// <summary>
        /// List of assemblies to scan for AutoMapper profiles.
        /// </summary>
        public List<Assembly> AutomapperAssemblies { get; }

        /// <summary>
        /// List of assemblies to scan for view components.
        /// </summary>
        public List<Assembly> ViewAssemblies { get; }
    }
}