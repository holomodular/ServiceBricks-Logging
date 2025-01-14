using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;
using ServiceBricks.Logging.SqlServer;

namespace ServiceBricks.Xunit
{
    public class StartupSqlServer : ServiceBricks.Startup
    {
        public StartupSqlServer(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksLoggingSqlServer(Configuration);
            services.AddServiceBricksComplete(Configuration);

            // Remove all background tasks/timers for unit testing
            var logtimer = services.Where(x => x.ImplementationType == typeof(CustomLoggerWriteMessageTimer)).FirstOrDefault();
            if (logtimer != null)
                services.Remove(logtimer);

            // Register TestManager
            services.AddScoped<ITestManager<LogMessageDto>, LogMessageTestManager>();
            services.AddScoped<ITestManager<WebRequestMessageDto>, WebRequestMessageTestManager>();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
        }
    }
}