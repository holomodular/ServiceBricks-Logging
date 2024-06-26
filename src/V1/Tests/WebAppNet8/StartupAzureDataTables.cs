using ServiceBricks;
using ServiceBricks.Logging.AzureDataTables;
using WebApp.Extensions;

namespace WebApp
{
    public class StartupAzureDataTables
    {
        public StartupAzureDataTables(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual IConfiguration Configuration { get; set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksLoggingAzureDataTables(Configuration);
            services.AddCustomWebsite(Configuration);
            services.AddServiceBricksComplete();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            app.StartServiceBricks();
            app.StartServiceBricksLoggingAzureDataTables();
            app.StartCustomWebsite(webHostEnvironment);
            var logger = app.ApplicationServices.GetRequiredService<ILogger<StartupAzureDataTables>>();
            logger.LogInformation("Application Started");
        }
    }
}