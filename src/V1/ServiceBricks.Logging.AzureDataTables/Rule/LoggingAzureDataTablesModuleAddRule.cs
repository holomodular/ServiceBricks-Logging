using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class LoggingAzureDataTablesModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<LoggingAzureDataTablesModule>),
                typeof(LoggingAzureDataTablesModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<LoggingAzureDataTablesModule>),
                typeof(LoggingAzureDataTablesModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<LoggingAzureDataTablesModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            //var configuration = e.Configuration;

            // AI: Configure all options for the module

            // AI: Add storage services for the module. Each domain object should have its own storage repository
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            // AI: Add API services for the module. Each DTO should have two registrations, one for the generic IApiService<> and one for the named interface
            services.AddScoped<IApiService<LogMessageDto>, LogMessageApiService>();
            services.AddScoped<ILogMessageApiService, LogMessageApiService>();

            services.AddScoped<IApiService<WebRequestMessageDto>, WebRequestMessageApiService>();
            services.AddScoped<IWebRequestMessageApiService, WebRequestMessageApiService>();

            // AI: Register mappings
            LogMessageMappingProfile.Register(MapperRegistry.Instance);
            WebRequestMessageMappingProfile.Register(MapperRegistry.Instance);

            // AI: Register business rules for the module
            DomainCreateDateRule<LogMessage>.Register(BusinessRuleRegistry.Instance);
            LogMessageCreateRule.Register(BusinessRuleRegistry.Instance);
            LogMessageQueryRule.Register(BusinessRuleRegistry.Instance);

            DomainCreateDateRule<WebRequestMessage>.Register(BusinessRuleRegistry.Instance);
            WebRequestMessageCreateRule.Register(BusinessRuleRegistry.Instance);
            WebRequestMessageQueryRule.Register(BusinessRuleRegistry.Instance);

            return response;
        }
    }
}