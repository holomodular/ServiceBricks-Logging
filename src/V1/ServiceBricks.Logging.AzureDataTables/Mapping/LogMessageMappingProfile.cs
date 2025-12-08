using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is a mapping profile for the LogMessage domain object.
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
                    d.StorageKey =
                        s.PartitionKey +
                        StorageAzureDataTablesConstants.STORAGEKEY_DELIMITER +
                        s.RowKey;
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
                    if (!string.IsNullOrEmpty(s.StorageKey))
                    {
                        string[] tempStorageKey = s.StorageKey.Split(StorageAzureDataTablesConstants.STORAGEKEY_DELIMITER);
                        if (tempStorageKey.Length >= 1)
                            d.PartitionKey = tempStorageKey[0];
                        else
                            d.PartitionKey = string.Empty;
                        if (tempStorageKey.Length >= 2)
                            d.RowKey = tempStorageKey[1];
                        else
                            d.RowKey = string.Empty;
                    }
                    else
                    {
                        d.PartitionKey = string.Empty;
                        d.RowKey = string.Empty;
                    }
                    d.UserStorageKey = s.UserStorageKey;
                });
        }
    }
}