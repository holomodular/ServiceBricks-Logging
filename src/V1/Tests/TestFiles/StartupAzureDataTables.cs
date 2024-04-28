using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;
using ServiceBricks.Logging.AzureDataTables;

namespace ServiceBricks.Xunit
{
    public class StartupAzureDataTables : ServiceBricks.Startup
    {
        public StartupAzureDataTables(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksLoggingAzureDataTables(Configuration);

            // Remove all background tasks/timers for unit testing
            var logtimer = services.Where(x => x.ImplementationType == typeof(LoggingWriteMessageTimer)).FirstOrDefault();
            if (logtimer != null)
                services.Remove(logtimer);

            // Register TestManager
            services.AddScoped<ITestManager<LogMessageDto>, LogMessageTestManager>();
            services.AddScoped<ITestManager<WebRequestMessageDto>, WebRequestMessageTestManager>();

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
            app.StartServiceBricksLoggingAzureDataTables();
        }
    }
}