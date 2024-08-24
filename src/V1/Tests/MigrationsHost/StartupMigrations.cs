using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;
using ServiceBricks.Logging.Postgres;
using ServiceBricks.Logging.Sqlite;
using ServiceBricks.Logging.SqlServer;

namespace ServiceBricks.Xunit
{
    public class StartupMigrations : ServiceBricks.Startup
    {
        public StartupMigrations(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);

            //**************************
            //UNCOMMENT THE ONE YOU NEED
            //**************************
            //services.AddServiceBricksLoggingPostgres(Configuration);
            services.AddServiceBricksLoggingSqlServer(Configuration);
            //services.AddServiceBricksLoggingSqlite(Configuration);

            // Remove all background tasks/timers for unit testing
            var logtimer = services.Where(x => x.ImplementationType == typeof(CustomLoggerWriteMessageTimer)).FirstOrDefault();
            if (logtimer != null)
                services.Remove(logtimer);

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();

            //**************************
            //UNCOMMENT THE ONE YOU NEED
            //**************************
            //app.StartServiceBricksLoggingPostgres();
            app.StartServiceBricksLoggingSqlServer();
            //app.StartServiceBricksLoggingSqlite();
        }
    }
}