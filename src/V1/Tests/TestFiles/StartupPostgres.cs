using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;
using ServiceBricks.Logging.Postgres;

namespace ServiceBricks.Xunit
{
    public class StartupPostgres : ServiceBricks.Startup
    {
        public StartupPostgres(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksLoggingPostgres(Configuration);

            // Remove all background tasks/timers for unit testing
            var logtimer = services.Where(x => x.ImplementationType == typeof(CustomLoggerWriteMessageTimer)).FirstOrDefault();
            if (logtimer != null)
                services.Remove(logtimer);

            // Register TestManager
            services.AddScoped<ITestManager<LogMessageDto>, LogMessageTestManagerPostgres>();
            services.AddScoped<ITestManager<WebRequestMessageDto>, WebRequestMessageTestManagerPostgres>();

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
            app.StartServiceBricksLoggingPostgres();
        }
    }
}