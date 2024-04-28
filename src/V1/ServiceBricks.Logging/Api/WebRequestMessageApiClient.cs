using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    public class WebRequestMessageApiClient : ApiClient<WebRequestMessageDto>, IWebRequestMessageApiClient
    {
        protected readonly IConfiguration _configuration;

        public WebRequestMessageApiClient(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(loggerFactory, httpClientFactory, configuration.GetApiConfig(LoggingConstants.APPSETTING_CLIENT_APICONFIG))
        {
            ApiResource = @"Logging/WebRequestMessage";
        }
    }
}