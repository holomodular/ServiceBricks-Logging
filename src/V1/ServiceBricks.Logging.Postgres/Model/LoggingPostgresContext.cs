using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Postgres
{
    // dotnet ef migrations add LoggingV1 --context LoggingPostgresContext --startup-project ../Test/MigrationsHost

    /// <summary>
    /// The database context for Logging.
    /// </summary>
    public partial class LoggingPostgresContext : DbContext
    {
        protected DbContextOptions<LoggingPostgresContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingPostgresContext() : base()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<LoggingPostgresContext>();
            string connectionString = configuration.GetPostgresConnectionString(
                LoggingPostgresConstants.APPSETTING_CONNECTION_STRING);
            builder.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(LoggingPostgresContext).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public LoggingPostgresContext(DbContextOptions<LoggingPostgresContext> options) : base(options)
        {
            _options = options;
        }

        public virtual DbSet<LogMessage> LogMessages { get; set; }
        public virtual DbSet<WebRequestMessage> WebRequestMessages { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Set default schema
            builder.HasDefaultSchema(LoggingPostgresConstants.DATABASE_SCHEMA_NAME);

            builder.Entity<LogMessage>().ToTable("LogMessage").HasKey(key => key.Key);

            builder.Entity<WebRequestMessage>().ToTable("WebRequestMessage").HasKey(key => key.Key);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().AddAppSettingsConfig().Build();
                string connectionString = configuration.GetPostgresConnectionString(LoggingPostgresConstants.APPSETTING_CONNECTION_STRING);
                optionsBuilder.UseNpgsql(connectionString, x =>
                {
                    x.MigrationsAssembly(typeof(LoggingPostgresContext).Assembly.GetName().Name);
                    x.EnableRetryOnFailure();
                });
            }
        }
    }
}