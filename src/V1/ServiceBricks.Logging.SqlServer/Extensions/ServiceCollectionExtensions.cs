using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.SqlServer
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging SqlServer module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging SqlServer module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingSqlServerModule), new LoggingSqlServerModule());

            // AI: Add parent module
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<LoggingSqlServerContext>();
            string connectionString = configuration.GetSqlServerConnectionString(
                LoggingSqlServerConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlServer(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            services.Configure<DbContextOptions<LoggingSqlServerContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingSqlServerContext>>(builder.Options);
            services.AddDbContext<LoggingSqlServerContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add storage services for the module. Each domain object should have its own storage repository.
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            return services;
        }
    }
}