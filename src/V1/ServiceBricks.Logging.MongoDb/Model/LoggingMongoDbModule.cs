﻿using System.Reflection;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is the logging MongoDb module.
    /// </summary>
    public partial class LoggingMongoDbModule : ServiceBricks.Module
    {
        /// <summary>
        /// Instance.
        /// </summary>
        public static LoggingMongoDbModule Instance = new LoggingMongoDbModule();

        /// <summary>
        /// Constructor
        /// </summary>
        public LoggingMongoDbModule()
        {
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingMongoDbModule).Assembly
            };
            DependentModules = new List<IModule>()
            {
                new LoggingModule()
            };
        }
    }
}