using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class LoggingCosmosModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<LoggingCosmosModule>),
                typeof(LoggingCosmosModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<LoggingCosmosModule>),
                typeof(LoggingCosmosModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<LoggingCosmosModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            var configuration = e.Configuration;

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<LoggingCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(
                LoggingCosmosConstants.APPSETTING_CONNECTION_STRING);
            string database = configuration.GetCosmosDatabase(
                LoggingCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);
            services.Configure<DbContextOptions<LoggingCosmosContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingCosmosContext>>(builder.Options);
            services.AddDbContext<LoggingCosmosContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Storage Services for the module for each domain object
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
            DomainQueryPropertyRenameRule<LogMessage>.Register(BusinessRuleRegistry.Instance, "StorageKey", "Key");
            LogMessageCreateRule.Register(BusinessRuleRegistry.Instance);

            DomainCreateDateRule<WebRequestMessage>.Register(BusinessRuleRegistry.Instance);
            DomainQueryPropertyRenameRule<WebRequestMessage>.Register(BusinessRuleRegistry.Instance, "StorageKey", "Key");
            WebRequestMessageCreateRule.Register(BusinessRuleRegistry.Instance);

            return response;
        }
    }
}