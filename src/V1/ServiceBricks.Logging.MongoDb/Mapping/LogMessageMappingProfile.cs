namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is an mapping profile for the LogMessage domain object.
    /// </summary>
    public partial class LogMessageMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<LogMessage, LogMessageDto>(
                (s, d) =>
                {
                    d.Application = s.Application;
                    d.Category = s.Category;
                    d.CreateDate = s.CreateDate;
                    d.Exception = s.Exception;
                    d.Level = s.Level;
                    d.Message = s.Message;
                    d.Path = s.Path;
                    d.Properties = s.Properties;
                    d.Server = s.Server;
                    d.StorageKey = s.Key;
                    d.UserStorageKey = s.UserStorageKey;
                });

            registry.Register<LogMessageDto, LogMessage>(
                (s, d) =>
                {
                    d.Application = s.Application;
                    d.Category = s.Category;
                    //d.CreateDate ignore by rule
                    d.Exception = s.Exception;
                    d.Level = s.Level;
                    d.Message = s.Message;
                    d.Path = s.Path;
                    d.Properties = s.Properties;
                    d.Server = s.Server;
                    d.Key = s.StorageKey;
                    d.UserStorageKey = s.UserStorageKey;
                });
        }
    }
}