using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Postgres
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksLoggingPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingPostgresModule), new LoggingPostgresModule());

            // Add Core Logging
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // Register Database
            var builder = new DbContextOptionsBuilder<LoggingPostgresContext>();
            string connectionString = configuration.GetPostgresConnectionString(
                LoggingPostgresConstants.APPSETTING_DATABASE_CONNECTION);
            builder.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            services.Configure<DbContextOptions<LoggingPostgresContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingPostgresContext>>(builder.Options);
            services.AddDbContext<LoggingPostgresContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            return services;
        }
    }
}