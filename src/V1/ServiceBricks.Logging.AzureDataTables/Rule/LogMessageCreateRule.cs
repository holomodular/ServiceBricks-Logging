using ServiceBricks.Storage.AzureDataTables;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is a business rule for creating a LogMessage domain object. It will set the Key, PartitionKey, and RowKey.
    /// </summary>
    public sealed class LogMessageCreateRule : BusinessRule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LogMessageCreateRule()
        {
            Priority = PRIORITY_LOW;
        }

        /// <summary>
        /// Register the rule
        /// </summary>
        /// <param name="registry"></param>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(DomainCreateBeforeEvent<LogMessage>),
                typeof(LogMessageCreateRule));
        }

        /// <summary>
        /// Unregister the rule
        /// </summary>
        /// <param name="registry"></param>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
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

            // AI: Make sure the context object is the correct type
            if (context == null || context.Object == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }
            var e = context.Object as DomainCreateBeforeEvent<LogMessage>;
            if (e == null || e.DomainObject == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Set the Key, PartitionKey, and RowKey
            var item = e.DomainObject;
            item.Key = Guid.NewGuid();

            // AI: Set the PartitionKey to be the year, month and day so that the data is partitioned
            item.PartitionKey = item.CreateDate.ToString("yyyyMMdd");

            // AI: Set the RowKey to be the reverse date and time so that the newest items are at the top when querying
            var reverseDate = DateTimeOffset.MaxValue.Ticks - item.CreateDate.Ticks;
            item.RowKey =
                reverseDate.ToString("d19") +
                StorageAzureDataTablesConstants.KEY_DELIMITER +
                item.Key.ToString();

            return response;
        }
    }
}