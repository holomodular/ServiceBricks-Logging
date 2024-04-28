using AutoMapper;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is an API service for the LogMessage domain object.
    /// </summary>
    public class LogMessageApiService : ApiService<LogMessage, LogMessageDto>, ILogMessageApiService
    {
        public LogMessageApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<LogMessage> repository) : base(mapper, businessRuleService, repository)
        {
        }
    }
}