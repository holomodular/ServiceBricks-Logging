using ServiceBricks;
using ServiceBricks.Logging.SqlServer;
using WebApp.Extensions;

namespace WebApp
{
    public class StartupSqlServer
    {
        public StartupSqlServer(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual IConfiguration Configuration { get; set; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceBricks(Configuration);
            services.AddServiceBricksLoggingSqlServer(Configuration);
            ModuleRegistry.Instance.Register(new WebApp.Model.WebAppModule()); // Just for automapper registration
            services.AddServiceBricksComplete(Configuration);
            services.AddCustomWebsite(Configuration);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
        {
            app.StartServiceBricks();
            app.StartCustomWebsite(webHostEnvironment);
            var logger = app.ApplicationServices.GetRequiredService<ILogger<StartupSqlServer>>();
            logger.LogInformation("Application Started");
        }
    }
}