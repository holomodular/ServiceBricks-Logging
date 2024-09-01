using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is an exposed REST API controller for the LogMessageDto data transfter object.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/Logging/LogMessage")]
    [Produces("application/json")]
    public partial class LogMessageApiController : AdminPolicyApiController<LogMessageDto>, ILogMessageApiController
    {
        protected readonly ILogMessageApiService _logMessageApiService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logMessageApiService"></param>
        /// <param name="apiOptions"></param>
        public LogMessageApiController(
            ILogMessageApiService logMessageApiService,
            IOptions<ApiOptions> apiOptions)
            : base(logMessageApiService, apiOptions)
        {
            _logMessageApiService = logMessageApiService;
        }
    }
}