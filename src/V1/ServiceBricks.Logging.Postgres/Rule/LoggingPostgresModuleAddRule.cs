using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Postgres
{
    /// <summary>
    /// This rule is executed when the ServiceBricks module is added.
    /// </summary>
    public sealed class LoggingPostgresModuleAddRule : BusinessRule
    {
        /// <summary>
        /// Register the rule
        /// </summary>
        public static void Register(IBusinessRuleRegistry registry)
        {
            registry.Register(
                typeof(ModuleAddEvent<LoggingPostgresModule>),
                typeof(LoggingPostgresModuleAddRule));
        }

        /// <summary>
        /// UnRegister the rule.
        /// </summary>
        public static void UnRegister(IBusinessRuleRegistry registry)
        {
            registry.UnRegister(
                typeof(ModuleAddEvent<LoggingPostgresModule>),
                typeof(LoggingPostgresModuleAddRule));
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
            var e = context.Object as ModuleAddEvent<LoggingPostgresModule>;
            if (e == null || e.DomainObject == null || e.ServiceCollection == null)
            {
                response.AddMessage(ResponseMessage.CreateError(LocalizationResource.PARAMETER_MISSING, "context"));
                return response;
            }

            // AI: Perform logic
            var services = e.ServiceCollection;
            var configuration = e.Configuration;

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<LoggingPostgresContext>();
            string connectionString = configuration.GetPostgresConnectionString(
                LoggingPostgresConstants.APPSETTING_CONNECTION_STRING);
            builder.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            services.Configure<DbContextOptions<LoggingPostgresContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingPostgresContext>>(builder.Options);
            services.AddDbContext<LoggingPostgresContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add storage services for the module. Each domain object should have its own storage repository.
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            return response;
        }
    }
}