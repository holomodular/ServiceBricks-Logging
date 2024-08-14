using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Sqlite
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging Sqlite module to the service collection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging Sqlite module to the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingSqliteModule), new LoggingSqliteModule());

            // AI: Add parent module
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<LoggingSqliteContext>();
            string connectionString = configuration.GetSqliteConnectionString(
                LoggingSqliteConstants.APPSETTING_CONNECTION_STRING);
            builder.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
            });
            services.Configure<DbContextOptions<LoggingSqliteContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingSqliteContext>>(builder.Options);
            services.AddDbContext<LoggingSqliteContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add storage services for the module. Each domain object should have its own storage repository.
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            return services;
        }
    }
}