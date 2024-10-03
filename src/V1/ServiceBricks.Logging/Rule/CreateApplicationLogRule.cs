using AutoMapper;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This business rule occurs when an CreateApplicationLogBroadcast servicebus event is received.
    /// </summary>
    public sealed class CreateApplicationLogRule : BusinessRule
    {
        private readonly ILogMessageApiService _logMessageApiService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logMessageApiService"></param>
        /// <param name="mapper"></param>
        public CreateApplicationLogRule(
            ILogMessageApiService logMessageApiService,
            IMapper mapper)
        {
            _mapper = mapper;
            _logMessageApiService = logMessageApiService;
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register the service bus broadcast event.
        /// </summary>
        /// <param name="registry"></param>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(CreateApplicationLogBroadcast),
                typeof(CreateApplicationLogRule));
        }

        /// <summary>
        /// Register the service bus broadcast event.
        /// </summary>
        /// <param name="registry"></param>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(CreateApplicationLogBroadcast),
                typeof(CreateApplicationLogRule));
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IResponse ExecuteRule(IBusinessRuleContext context)
        {
            var response = new Response();

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var e = context.Object as CreateApplicationLogBroadcast;
            if (e == null || e.DomainObject == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Map the ApplicationLogBroadcast to a LogMessageDto
            var message = _mapper.Map<LogMessageDto>(e.DomainObject);

            // AI: Call the API service to create the log message
            var respCreate = _logMessageApiService.Create(message);

            // AI: Copy the API response to the business rule response
            response.CopyFrom(respCreate);
            return response;
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<IResponse> ExecuteRuleAsync(IBusinessRuleContext context)
        {
            var response = new Response();

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var e = context.Object as CreateApplicationLogBroadcast;
            if (e == null || e.DomainObject == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Map the ApplicationLogBroadcast to a LogMessageDto
            var message = _mapper.Map<LogMessageDto>(e.DomainObject);

            // AI: Call the API service to create the log message
            var respCreate = await _logMessageApiService.CreateAsync(message);

            // AI: Copy the API response to the business rule response
            response.CopyFrom(respCreate);

            return response;
        }
    }
}