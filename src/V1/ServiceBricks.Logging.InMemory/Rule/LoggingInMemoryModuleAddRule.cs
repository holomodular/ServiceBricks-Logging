using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class LoggingInMemoryModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<LoggingInMemoryModule>),
                typeof(LoggingInMemoryModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<LoggingInMemoryModule>),
                typeof(LoggingInMemoryModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<LoggingInMemoryModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            var configuration = e.Configuration;

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<LoggingInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            services.Configure<DbContextOptions<LoggingInMemoryContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingInMemoryContext>>(builder.Options);
            services.AddDbContext<LoggingInMemoryContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            return response;
        }
    }
}