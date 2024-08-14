using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// Extensions to add the ServiceBricks Logging InMemory module to the IServiceCollection.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ServiceBricks Logging InMemory module to the IServiceCollection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBricksLoggingInMemory(this IServiceCollection services, IConfiguration configuration)
        {
            // AI: Add the module to the ModuleRegistry
            ModuleRegistry.Instance.RegisterItem(typeof(LoggingInMemoryModule), new LoggingInMemoryModule());

            // AI: Add the parent module
            services.AddServiceBricksLoggingEntityFrameworkCore(configuration);

            // AI: Register the database for the module
            var builder = new DbContextOptionsBuilder<LoggingInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            services.Configure<DbContextOptions<LoggingInMemoryContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<LoggingInMemoryContext>>(builder.Options);
            services.AddDbContext<LoggingInMemoryContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // AI: Add the storage services for the module for each domain object
            services.AddScoped<IStorageRepository<LogMessage>, LoggingStorageRepository<LogMessage>>();
            services.AddScoped<IStorageRepository<WebRequestMessage>, LoggingStorageRepository<WebRequestMessage>>();

            return services;
        }
    }
}