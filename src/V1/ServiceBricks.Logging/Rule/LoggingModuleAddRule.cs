using Microsoft.Extensions.DependencyInjection;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class LoggingModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<LoggingModule>),
                typeof(LoggingModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<LoggingModule>),
                typeof(LoggingModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<LoggingModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            var configuration = e.Configuration;

            // AI: Add any custom requirements for the module
            services.AddLogging();

            // AI: Add hosted services for the module
            services.AddHostedService<CustomLoggerWriteMessageTimer>();
            services.AddHostedService<WebRequestMessageWriteTimer>();

            // AI: Add workers for tasks in the module
            services.AddScoped<CustomLoggerWriteMessageTask.Worker>();
            services.AddScoped<WebRequestMessageWriteTask.Worker>();

            // AI: Configure all options for the module
            services.Configure<WebRequestMessageOptions>(configuration.GetSection(LoggingConstants.APPSETTING_WEBREQUESTMESSAGE));

            // AI: Add API Controllers for each DTO in the module
            services.AddScoped<IApiController<LogMessageDto>, LogMessageApiController>();
            services.AddScoped<ILogMessageApiController, LogMessageApiController>();

            services.AddScoped<IApiController<WebRequestMessageDto>, WebRequestMessageApiController>();
            services.AddScoped<IWebRequestMessageApiController, WebRequestMessageApiController>();

            // AI: Add any miscellaneous services for the module
            services.AddTransient<WebRequestMessageMiddleware>();

            // AI: Register mappings
            ApplicationLogDtoMappingProfile.Register(MapperRegistry.Instance);

            // AI: Register business rules for the module
            CreateApplicationLogRule.Register(BusinessRuleRegistry.Instance);

            return response;
        }
    }
}