namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is an API service for the WebRequestMessage domain object.
    /// </summary>
    public partial class WebRequestMessageApiService : ApiService<WebRequestMessage, WebRequestMessageDto>, IWebRequestMessageApiService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="businessRuleService"></param>
        /// <param name="repository"></param>
        public WebRequestMessageApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<WebRequestMessage> repository) : base(mapper, businessRuleService, repository)
        {
        }
    }
}