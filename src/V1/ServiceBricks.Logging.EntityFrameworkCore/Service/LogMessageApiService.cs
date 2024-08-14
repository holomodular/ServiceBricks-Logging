using AutoMapper;

namespace ServiceBricks.Logging.EntityFrameworkCore
{
    /// <summary>
    /// This is an API service for the LogMessage domain object.
    /// </summary>
    public partial class LogMessageApiService : ApiService<LogMessage, LogMessageDto>, ILogMessageApiService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="businessRuleService"></param>
        /// <param name="repository"></param>
        public LogMessageApiService(
            IMapper mapper,
            IBusinessRuleService businessRuleService,
            IDomainRepository<LogMessage> repository)
            : base(mapper, businessRuleService, repository)
        {
        }
    }
}