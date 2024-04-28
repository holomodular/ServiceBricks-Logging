using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// IServiceCollection extensions for the Log module.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceBricksLoggingInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            // Add to module registry for automapper
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingInMemoryModule), new LoggingInMemoryModule());

            // Register Database
            var builder = new DbContextOptionsBuilder<LoggingInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            services.Configure<DbContextOptions<LoggingInMemoryContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingInMemoryContext>>(builder.Options);
            services.AddDbContext<LoggingInMemoryContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Storage Services
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            // Add Core Logging
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            return services;
        }
    }
}