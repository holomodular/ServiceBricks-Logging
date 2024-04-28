using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    public class LogMessageApiClient : ApiClient<LogMessageDto>, ILogMessageApiClient
    {
        protected readonly IConfiguration _configuration;

        public LogMessageApiClient(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(loggerFactory, httpClientFactory, configuration.GetApiConfig(LoggingConstants.APPSETTING_CLIENT_APICONFIG))
        {
            ApiResource = @"Logging/LogMessage";
        }
    }
}