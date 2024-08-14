using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is a business rule for the LogMessage domain object used to modify the query before it is executed.
    /// </summary>
    public sealed class LogMessageQueryRule : BusinessRule
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public LogMessageQueryRule(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LogMessageQueryRule>();
            Priority = PRIORITY_NORMAL;
        }

        /// <summary>
        /// Register the business rule to the DomainQueryBeforeEvent.
        /// </summary>
        /// <param name="registry"></param>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            registry.RegisterItem(
                typeof(DomainQueryBeforeEvent<LogMessage>),
                typeof(LogMessageQueryRule));
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
                if (context.Object is DomainQueryBeforeEvent<LogMessage> ei)
                {
                    var item = ei.DomainObject;
                    if (ei.ServiceQueryRequest == null || ei.ServiceQueryRequest.Filters == null)
                        return response;

                    // AI: Iterate through the filters and modify the property name for StorageKey and change it to Key
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
                                // AI: Iterate through the values. Split each one using the delimiter and re-set the value to use the second part. See LogMessageCreateRule for more information.
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

        /// <summary>
        /// Execute the business rule.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<IResponse> ExecuteRuleAsync(IBusinessRuleContext context)
        {
            return Task.FromResult<IResponse>(ExecuteRule(context));
        }
    }
}