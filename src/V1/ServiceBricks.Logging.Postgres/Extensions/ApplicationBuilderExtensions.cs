using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.Postgres
{
    /// <summary>
    /// Extensions to start the ServiceBricks Logging Postgres module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to check if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Logging Postgres module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksLoggingPostgres(this IApplicationBuilder applicationBuilder)
        {
            // AI: Migrate the database
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LoggingPostgresContext>();
                context.Database.Migrate();
                context.SaveChanges();
            }

            // AI: Flag the module as started
            ModuleStarted = true;

            // AI: Start parent module
            applicationBuilder.StartServiceBricksLoggingEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}