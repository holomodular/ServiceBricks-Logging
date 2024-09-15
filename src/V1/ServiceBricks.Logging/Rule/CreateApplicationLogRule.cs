using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This business rule occurs when an CreateApplicationLogBroadcast servicebus event is received.
    /// </summary>
    public sealed class CreateApplicationLogRule : BusinessRule
    {
        private readonly ILogMessageApiService _logMessageApiService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateApplicationLogRule> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="logMessageApiService"></param>
        /// <param name="mapper"></param>
        public CreateApplicationLogRule(
            ILoggerFactory loggerFactory,
            ILogMessageApiService logMessageApiService,
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<CreateApplicationLogRule>();
            _mapper = mapper;
            _logMessageApiService = logMessageApiService;
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register the service bus broadcast event.
        /// </summary>
        /// <param name="serviceBus"></param>
        public static void RegisterServiceBus(IServiceBus serviceBus)
        {
            serviceBus.Subscribe(
                typeof(CreateApplicationLogBroadcast),
                typeof(CreateApplicationLogRule));
        }

        /// <summary>
        /// Register the service bus broadcast event.
        /// </summary>
        /// <param name="serviceBus"></param>
        public static void UnRegisterServiceBus(IServiceBus serviceBus)
        {
            serviceBus.Unsubscribe(
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

            try
            {
                // AI: Make sure the context object is the correct type
                var e = context.Object as CreateApplicationLogBroadcast;
                if (e == null || e.DomainObject == null)
                    return response;

                // AI: Map the ApplicationLogBroadcast to a LogMessageDto
                var message = _mapper.Map<LogMessageDto>(e.DomainObject);

                // AI: Call the API service to create the log message
                var respCreate = _logMessageApiService.Create(message);

                // AI: Copy the API response to the business rule response
                response.CopyFrom(respCreate);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_BUSINESS_RULE));
                return response;
            }
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<IResponse> ExecuteRuleAsync(IBusinessRuleContext context)
        {
            var response = new Response();

            try
            {
                // AI: Make sure the context object is the correct type
                var e = context.Object as CreateApplicationLogBroadcast;
                if (e == null || e.DomainObject == null)
                    return response;

                // AI: Map the ApplicationLogBroadcast to a LogMessageDto
                var message = _mapper.Map<LogMessageDto>(e.DomainObject);

                // AI: Call the API service to create the log message
                var respCreate = await _logMessageApiService.CreateAsync(message);

                // AI: Copy the API response to the business rule response
                response.CopyFrom(respCreate);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_BUSINESS_RULE));
                return response;
            }
        }
    }
}