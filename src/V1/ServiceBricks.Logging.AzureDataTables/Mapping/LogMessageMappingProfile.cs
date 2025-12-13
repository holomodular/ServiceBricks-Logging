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
                    d.UserStorageKey = s.UserStorageKey;
                    var reverseDate = DateTimeOffset.MaxValue.Ticks - s.CreateDate.Ticks;
                    d.StorageKey =
                        s.CreateDate.ToString("yyyyMMdd") +
                        StorageAzureDataTablesConstants.STORAGEKEY_DELIMITER +
                        reverseDate.ToString("d19") +
                        StorageAzureDataTablesConstants.KEY_DELIMITER +
                        s.Key.ToString();
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
                    d.UserStorageKey = s.UserStorageKey;
                    if (!string.IsNullOrEmpty(s.StorageKey))
                    {
                        string[] tempStorageKey = s.StorageKey.Split(StorageAzureDataTablesConstants.STORAGEKEY_DELIMITER);
                        if (tempStorageKey.Length >= 1)
                            d.PartitionKey = tempStorageKey[0];
                        if (tempStorageKey.Length >= 2)
                        {
                            string[] splitRowKey = tempStorageKey[1].Split(StorageAzureDataTablesConstants.KEY_DELIMITER);
                            if(splitRowKey.Length >= 1)
                            {
                                long tempReverseTicks;
                                if (long.TryParse(splitRowKey[0], out tempReverseTicks))
                                {
                                    long originalTicks = DateTimeOffset.MaxValue.Ticks - tempReverseTicks;
                                    d.CreateDate = new DateTimeOffset(originalTicks, TimeSpan.Zero);
                                }
                            }
                            if (splitRowKey.Length >= 2)
                            {
                                Guid tempKey;
                                if (Guid.TryParse(splitRowKey[1], out tempKey))
                                    d.Key = tempKey;
                            }
                            var reverseDate = DateTimeOffset.MaxValue.Ticks - d.CreateDate.Ticks;
                            d.RowKey =
                                reverseDate.ToString("d19") +
                                StorageAzureDataTablesConstants.KEY_DELIMITER +
                                d.Key.ToString();
                        }                            
                    }                    
                });
        }
    }
}