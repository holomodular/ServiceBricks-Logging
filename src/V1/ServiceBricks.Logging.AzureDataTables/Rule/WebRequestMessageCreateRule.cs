using Microsoft.Extensions.Logging;
using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is a business rule for the WebRequestMessage object to set the
    /// partitionkey and rowkey of the object before create.
    /// </summary>
    public partial class WebRequestMessageCreateRule : BusinessRule
    {
        /// <summary>
        /// Internal.
        /// </summary>
        protected readonly ILogger _logger;

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
        /// Register a rule for a domain object.
        /// </summary>
        public static void RegisterRule(IBusinessRuleRegistry registry)
        {
            registry.RegisterItem(
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
                if (context.Object is DomainCreateBeforeEvent<WebRequestMessage> ei)
                {
                    var item = ei.DomainObject;
                    item.Key = Guid.NewGuid();
                    item.PartitionKey = item.CreateDate.ToString("yyyyMM");
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
    }
}