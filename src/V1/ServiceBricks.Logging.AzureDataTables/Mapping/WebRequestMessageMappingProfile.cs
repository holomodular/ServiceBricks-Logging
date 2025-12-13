using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is a mapping profile for the WebRequestMessage domain object.
    /// </summary>
    public partial class WebRequestMessageMappingProfile
    {
        /// <summary>
        /// Register the mapping
        /// </summary>
        public static void Register(IMapperRegistry registry)
        {
            registry.Register<WebRequestMessage, WebRequestMessageDto>(
                (s, d) =>
                {
                    d.Application = s.Application;
                    d.CreateDate = s.CreateDate;
                    d.Exception = s.Exception;
                    d.RequestBody = s.RequestBody;
                    d.RequestContentLength = s.RequestContentLength;
                    d.RequestContentType = s.RequestContentType;
                    d.RequestCookies = s.RequestCookies;
                    d.RequestHasFormContentType = s.RequestHasFormContentType;
                    d.RequestHeaders = s.RequestHeaders;
                    d.RequestHost = s.RequestHost;
                    d.RequestIPAddress = s.RequestIPAddress;
                    d.RequestIsHttps = s.RequestIsHttps;
                    d.RequestMethod = s.RequestMethod;
                    d.RequestPath = s.RequestPath;
                    d.RequestPathBase = s.RequestPathBase;
                    d.RequestProtocol = s.RequestProtocol;
                    d.RequestQuery = s.RequestQuery;
                    d.RequestQueryString = s.RequestQueryString;
                    d.RequestRouteValues = s.RequestRouteValues;
                    d.RequestScheme = s.RequestScheme;
                    d.ResponseBody = s.ResponseBody;
                    d.ResponseContentLength = s.ResponseContentLength;
                    d.ResponseContentType = s.ResponseContentType;
                    d.ResponseCookies = s.ResponseCookies;
                    d.ResponseHeaders = s.ResponseHeaders;
                    d.ResponseStatusCode = s.ResponseStatusCode;
                    d.ResponseTotalMilliseconds = s.ResponseTotalMilliseconds;
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

            registry.Register<WebRequestMessageDto, WebRequestMessage>(
                (s, d) =>
                {
                    d.Application = s.Application;
                    //d.CreateDate ignore by rule
                    d.Exception = s.Exception;
                    d.RequestBody = s.RequestBody;
                    d.RequestContentLength = s.RequestContentLength;
                    d.RequestContentType = s.RequestContentType;
                    d.RequestCookies = s.RequestCookies;
                    d.RequestHasFormContentType = s.RequestHasFormContentType;
                    d.RequestHeaders = s.RequestHeaders;
                    d.RequestHost = s.RequestHost;
                    d.RequestIPAddress = s.RequestIPAddress;
                    d.RequestIsHttps = s.RequestIsHttps;
                    d.RequestMethod = s.RequestMethod;
                    d.RequestPath = s.RequestPath;
                    d.RequestPathBase = s.RequestPathBase;
                    d.RequestProtocol = s.RequestProtocol;
                    d.RequestQuery = s.RequestQuery;
                    d.RequestQueryString = s.RequestQueryString;
                    d.RequestRouteValues = s.RequestRouteValues;
                    d.RequestScheme = s.RequestScheme;
                    d.ResponseBody = s.ResponseBody;
                    d.ResponseContentLength = s.ResponseContentLength;
                    d.ResponseContentType = s.ResponseContentType;
                    d.ResponseCookies = s.ResponseCookies;
                    d.ResponseHeaders = s.ResponseHeaders;
                    d.ResponseStatusCode = s.ResponseStatusCode;
                    d.ResponseTotalMilliseconds = s.ResponseTotalMilliseconds;
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
                            if (splitRowKey.Length >= 1)
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
                        }
                        var reverseDate = DateTimeOffset.MaxValue.Ticks - d.CreateDate.Ticks;
                        d.RowKey = 
                            reverseDate.ToString("d19") +
                            StorageAzureDataTablesConstants.KEY_DELIMITER +
                            d.Key.ToString();
                    }
                });
        }
    }
}