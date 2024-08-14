using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;
using ServiceBricks.Logging.MongoDb;

namespace ServiceBricks.Xunit
{
    public class StartupMongoDb : ServiceBricks.Startup
    {
        public StartupMongoDb(IConfiguration configuration) : base(configuration)
        {
        }

        public virtual void ConfigureDevelopmentServices(IServiceCollection services)
        {
            base.CustomConfigureServices(services);
            services.AddSingleton(Configuration);
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksLoggingMongoDb(Configuration);

            // Remove all background tasks/timers for unit testing
            var logtimer = services.Where(x => x.ImplementationType == typeof(CustomLoggerWriteMessageTimer)).FirstOrDefault();
            if (logtimer != null)
                services.Remove(logtimer);

            // Register TestManager
            services.AddScoped<ITestManager<LogMessageDto>, LogMessageTestManagerMongoDb>();
            services.AddScoped<ITestManager<WebRequestMessageDto>, WebRequestMessageTestManagerMongoDb>();

            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            base.CustomConfigure(app);
            app.StartServiceBricks();
            app.StartServiceBricksLoggingMongoDb();
        }
    }
}