namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// This is an automapper profile for the LogMessage domain object.
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
                    d.StorageKey = s.Key.ToString();
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
                    Guid tempKey;
                    if (Guid.TryParse(s.StorageKey, out tempKey))
                        d.Key = tempKey;
                    d.UserStorageKey = s.UserStorageKey;
                });
        }
    }
}