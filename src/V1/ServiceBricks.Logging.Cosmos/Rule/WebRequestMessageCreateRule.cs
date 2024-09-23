﻿using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// This is a business rule for creating a WebRequestMessage domain object. It will set the Key and PartitionKey.
    /// </summary>
    public sealed class WebRequestMessageCreateRule : BusinessRule
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public WebRequestMessageCreateRule(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<WebRequestMessageCreateRule>();
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register the rule
        /// </summary>
        /// <param name="registry"></param>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(DomainCreateBeforeEvent<WebRequestMessage>),
                typeof(WebRequestMessageCreateRule));
        }

        /// <summary>
        /// Unregister the rule
        /// </summary>
        /// <param name="registry"></param>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(DomainCreateBeforeEvent<WebRequestMessage>),
                typeof(WebRequestMessageCreateRule));
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
                if (context.Object is DomainCreateBeforeEvent<WebRequestMessage> e)
                {
                    // AI: Set the Key and PartitionKey
                    var item = e.DomainObject;
                    item.Key = Guid.NewGuid();

                    // AI: Set the PartitionKey to be the year, month and day so that the data is partitioned
                    item.PartitionKey = item.CreateDate.ToString("yyyyMMdd");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_BUSINESS_RULE));
            }

            return response;
        }

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<IResponse> ExecuteRuleAsync(IBusinessRuleContext context)
        {
            // AI: There is no async work, so just call the sync method
            return Task.FromResult<IResponse>(ExecuteRule(context));
        }
    }
}