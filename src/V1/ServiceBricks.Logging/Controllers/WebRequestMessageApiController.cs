using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is an exposed REST API controller for the WebRequestMessageDto data transfer object.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/Logging/WebRequestMessage")]
    [Produces("application/json")]
    public partial class WebRequestMessageApiController : AdminPolicyApiController<WebRequestMessageDto>, IWebRequestMessageApiController
    {
        protected readonly IWebRequestMessageApiService _webRequestMessageApiService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="WebRequestMessageApiService"></param>
        /// <param name="apiOptions"></param>
        public WebRequestMessageApiController(
            IWebRequestMessageApiService WebRequestMessageApiService,
            IOptions<ApiOptions> apiOptions)
            : base(WebRequestMessageApiService, apiOptions)
        {
            _webRequestMessageApiService = WebRequestMessageApiService;
        }
    }
}