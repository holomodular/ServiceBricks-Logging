using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class LoggingMongoDbModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<LoggingMongoDbModule>),
                typeof(LoggingMongoDbModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<LoggingMongoDbModule>),
                typeof(LoggingMongoDbModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<LoggingMongoDbModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            //var configuration = e.Configuration;

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiService>();
            services.AddScoped<ILogMessageApiService, LogMessageApiService>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiService>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiService>();

            // AI: Add business rules for the module
            DomainCreateDateRule<LogMessage>.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<LogMessage>.Register(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            DomainCreateDateRule<WebRequestMessage>.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<WebRequestMessage>.Register(BusinessRuleRegistry.Instance, "StorageKey", "Key");

            return response;
        }
    }
}