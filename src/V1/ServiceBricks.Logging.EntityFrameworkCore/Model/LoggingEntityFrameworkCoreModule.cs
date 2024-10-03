﻿using System.Reflection;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// The module definition for the ServiceBricks Logging EntityFrameworkCore module.
    /// </summary>
    public partial class LoggingEntityFrameworkCoreModule : ServiceBricks.Module
    {
        public static LoggingEntityFrameworkCoreModule Instance = new LoggingEntityFrameworkCoreModule();

        /// <summary>
        /// Constructor for the ServiceBricks Logging EntityFrameworkCore module.
        /// </summary>
        public LoggingEntityFrameworkCoreModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingEntityFrameworkCoreModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new LoggingModule()
            };
        }
    }
}