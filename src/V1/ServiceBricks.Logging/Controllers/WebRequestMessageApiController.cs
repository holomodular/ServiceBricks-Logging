using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is an exposed REST API controller for the WebRequestMessage domain object.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/Logging/WebRequestMessage")]
    [Produces("application/json")]
    public class WebRequestMessageApiController : AdminPolicyApiController<WebRequestMessageDto>, IWebRequestMessageApiController
    {
        protected readonly IWebRequestMessageApiService _WebRequestMessageApiService;

        public WebRequestMessageApiController(
            IWebRequestMessageApiService WebRequestMessageApiService,
            IOptions<ApiOptions> apiOptions)
            : base(WebRequestMessageApiService, apiOptions)
        {
            _WebRequestMessageApiService = WebRequestMessageApiService;
        }
    }
}