using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This business rule occurs when an email needs to be created.
    /// </summary>
    public partial class CreateApplicationLogRule : BusinessRule
    {
        private readonly ILogMessageApiService _logMessageApiService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateApplicationLogRule> _logger;

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
        /// Register the business rule.
        /// </summary>
        public static void RegisterServiceBus(IServiceBus serviceBus)
        {
            serviceBus.Subscribe(
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
                var e = context.Object as CreateApplicationLogBroadcast;
                if (e == null || e.DomainObject == null)
                    return response;

                // Map and Create
                var message = _mapper.Map<LogMessageDto>(e.DomainObject);
                var respCreate = _logMessageApiService.Create(message);
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
                var e = context.Object as CreateApplicationLogBroadcast;
                if (e == null || e.DomainObject == null)
                    return response;

                // Map and Create
                var message = _mapper.Map<LogMessageDto>(e.DomainObject);
                var respCreate = await _logMessageApiService.CreateAsync(message);
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