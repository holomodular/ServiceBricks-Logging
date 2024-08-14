using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.Sqlite
{
    /// <summary>
    /// Extensions to start the ServiceBricks Logging Sqlite module.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Flag to check if the module has been started.
        /// </summary>
        public static bool ModuleStarted = false;

        /// <summary>
        /// Start the ServiceBricks Logging Sqlite module.
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartServiceBricksLoggingSqlite(this IApplicationBuilder applicationBuilder)
        {
            // AI: Migrate the database
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LoggingSqliteContext>();
                context.Database.Migrate();
                context.SaveChanges();
            }

            // AI: Set the module started flag
            ModuleStarted = true;

            // AI: Start the parent module
            applicationBuilder.StartServiceBricksLoggingEntityFrameworkCore();

            return applicationBuilder;
        }
    }
}