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
                    d.StorageKey =
                        s.PartitionKey +
                        StorageAzureDataTablesConstants.STORAGEKEY_DELIMITER +
                        s.RowKey;
                    d.UserStorageKey = s.UserStorageKey;
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