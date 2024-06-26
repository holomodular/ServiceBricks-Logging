﻿using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    public partial class WebRequestMessageQueryRule : BusinessRule
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public WebRequestMessageQueryRule(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<WebRequestMessageQueryRule>();
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register a rule for a domain object.
        /// </summary>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            registry.RegisterItem(
                typeof(DomainQueryBeforeEvent<WebRequestMessage>),
                typeof(WebRequestMessageQueryRule));
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
                if (context.Object is DomainQueryBeforeEvent<WebRequestMessage> ei)
                {
                    var item = ei.DomainObject;
                    if (ei.ServiceQueryRequest == null || ei.ServiceQueryRequest.Filters == null)
                        return response;
                    foreach (var filter in ei.ServiceQueryRequest.Filters)
                    {
                        if (filter.Properties != null &&
                            filter.Properties.Count > 0)
                        {
                            bool found = false;
                            for (int i = 0; i < filter.Properties.Count; i++)
                            {
                                if (string.Compare(filter.Properties[i], "StorageKey", true) == 0)
                                {
                                    found = true;
                                    filter.Properties[i] = "Key";
                                }
                            }
                            if (found)
                            {
                                if (filter.Values != null && filter.Values.Count > 0)
                                {
                                    for (int i = 0; i < filter.Values.Count; i++)
                                    {
                                        string[] split = filter.Values[i].Split(StorageAzureDataTablesConstants.KEY_DELIMITER);
                                        if (split.Length == 2)
                                        {
                                            filter.Values[i] = split[1];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.ERROR_BUSINESS_RULE));
            }

            return response;
        }
    }
}