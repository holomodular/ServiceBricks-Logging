using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is an exposed REST API controller for the LogMessage domain object.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/Logging/LogMessage")]
    [Produces("application/json")]
    public class LogMessageApiController : AdminPolicyApiController<LogMessageDto>, ILogMessageApiController
    {
        protected readonly ILogMessageApiService _logMessageApiService;

        public LogMessageApiController(
            ILogMessageApiService logMessageApiService,
            IOptions<ApiOptions> apiOptions)
            : base(logMessageApiService, apiOptions)
        {
            _logMessageApiService = logMessageApiService;
        }
    }
}