using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This class is an API client for the WebRequestMessageDto.
    /// </summary>
    public partial class WebRequestMessageApiClient : ApiClient<WebRequestMessageDto>, IWebRequestMessageApiClient
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public WebRequestMessageApiClient(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(loggerFactory, httpClientFactory, configuration.GetApiConfig(LoggingModelConstants.APPSETTING_CLIENT_APICONFIG))
        {
            ApiResource = @"Logging/WebRequestMessage";
        }
    }
}