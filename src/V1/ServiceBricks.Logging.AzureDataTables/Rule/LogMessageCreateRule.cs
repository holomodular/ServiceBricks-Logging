using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is a business rule for creating a LogMessage domain object. It will set the Key, PartitionKey, and RowKey.
    /// </summary>
    public sealed class LogMessageCreateRule : BusinessRule
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerFactory"></param>
        public LogMessageCreateRule(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LogMessageCreateRule>();
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register the business rule to the DomainCreateBeforeEvent.
        /// </summary>
        /// <param name="registry"></param>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            registry.RegisterItem(
                typeof(DomainCreateBeforeEvent<LogMessage>),
                typeof(LogMessageCreateRule));
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
                if (context.Object is DomainCreateBeforeEvent<LogMessage> e)
                {
                    // AI: Set the Key, PartitionKey, and RowKey
                    var item = e.DomainObject;
                    item.Key = Guid.NewGuid();

                    // AI: Set the PartitionKey to be the year and month so that the data is partitioned
                    item.PartitionKey = item.CreateDate.ToString("yyyyMM");

                    // AI: Set the RowKey to be the reverse date and time so that the newest items are at the top when querying
                    var reverseDate = DateTimeOffset.MaxValue.Ticks - item.CreateDate.Ticks;
                    item.RowKey =
                        reverseDate.ToString("d19") +
                        StorageAzureDataTablesConstants.KEY_DELIMITER +
                        item.Key.ToString();
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