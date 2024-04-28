using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Sqlite
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksLoggingSqlite(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingSqliteModule), new LoggingSqliteModule());

            // Add Core Logging
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // Register Database
            var builder = new DbContextOptionsBuilder<LoggingSqliteContext>();
            string connectionString = configuration.GetSqliteConnectionString(
                LoggingSqliteConstants.APPSETTING_DATABASE_CONNECTION);
            builder.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
            });
            services.Configure<DbContextOptions<LoggingSqliteContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingSqliteContext>>(builder.Options);
            services.AddDbContext<LoggingSqliteContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            return services;
        }
    }
}