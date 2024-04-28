using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.SqlServer
{
    // dotnet ef migrations add LoggingV1 --context LoggingSqlServerContext --startup-project ../Test/MigrationsHost

    /// <summary>
    /// The database context for Logging.
    /// </summary>
    public partial class LoggingSqlServerContext : DbContext
    {
        protected DbContextOptions<LoggingSqlServerContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingSqlServerContext() : base()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<LoggingSqlServerContext>();
            string connectionString = configuration.GetSqlServerConnectionString(
                LoggingSqlServerConstants.APPSETTING_DATABASE_CONNECTION);
            builder.UseSqlServer(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(LoggingSqlServerContext).Assembly.GetName().Name);
                x.EnableRetryOnFailure();
            });
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public LoggingSqlServerContext(DbContextOptions<LoggingSqlServerContext> options) : base(options)
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
            builder.HasDefaultSchema(LoggingSqlServerConstants.DATABASE_SCHEMA_NAME);

            builder.Entity<LogMessage>().ToTable("LogMessage").HasKey(key => key.Key);

            builder.Entity<WebRequestMessage>().ToTable("WebRequestMessage").HasKey(key => key.Key);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().AddAppSettingsConfig().Build();
                string connectionString = configuration.GetSqlServerConnectionString(LoggingSqlServerConstants.APPSETTING_DATABASE_CONNECTION);
                optionsBuilder.UseSqlServer(connectionString, x =>
                {
                    x.MigrationsAssembly(typeof(LoggingSqlServerContext).Assembly.GetName().Name);
                    x.EnableRetryOnFailure();
                });
            }
        }
    }
}