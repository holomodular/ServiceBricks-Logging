using System.Reflection;

namespace ServiceBricks.Logging.MongoDb
{
    public class LoggingMongoDbModule : IModule
    {
        public LoggingMongoDbModule()
        {
            AdminHtml = string.Empty;
            Name = "Logging MongoDB Brick";
            Description = @"The Logging MongoDB Brick implements the MongoDB provider.";
            AutomapperAssemblies = new List<Assembly>()
            {
                typeof(LoggingMongoDbModule).Assembly
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