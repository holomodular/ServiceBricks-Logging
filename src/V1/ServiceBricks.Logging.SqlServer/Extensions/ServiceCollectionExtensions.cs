using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.SqlServer
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksLoggingSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingSqlServerModule), new LoggingSqlServerModule());

            // Add Core Logging
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // Register Database
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

            // Storage Services
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            return services;
        }
    }
}