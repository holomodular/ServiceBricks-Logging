using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This class is an REST API client for the LogMessageDto.
    /// </summary>
    public partial class LogMessageApiClient : ApiClient<LogMessageDto>, ILogMessageApiClient
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public LogMessageApiClient(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
            : base(loggerFactory, httpClientFactory, configuration.GetApiConfig(LoggingModelConstants.APPSETTING_CLIENT_APICONFIG))
        {
            ApiResource = @"Logging/LogMessage";
        }
    }
}