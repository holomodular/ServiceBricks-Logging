namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a mapping profile for the ApplicationLogDto.
    /// </summary>
    public partial class ApplicationLogDtoMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<ApplicationLogDto, LogMessageDto>(
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
                    d.StorageKey = s.StorageKey;
                    d.UserStorageKey = s.UserStorageKey;
                });
        }
    }
}