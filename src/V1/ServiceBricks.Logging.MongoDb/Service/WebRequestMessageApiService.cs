using AutoMapper;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is an API service for the WebRequestMessage domain object.
    /// </summary>
    public class WebRequestMessageApiService : ApiService<WebRequestMessage, WebRequestMessageDto>, IWebRequestMessageApiService
    {
        public WebRequestMessageApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<WebRequestMessage> repository) : base(mapper, businessRuleService, repository)
        {
        }
    }
}