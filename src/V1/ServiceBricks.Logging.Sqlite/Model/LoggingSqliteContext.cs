using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Sqlite
{
    // dotnet ef migrations add LoggingV1 --context LoggingSqliteContext --startup-project ../Test/MigrationsHost

    /// <summary>
    /// The database context for Logging.
    /// </summary>
    public partial class LoggingSqliteContext : DbContext
    {
        protected DbContextOptions<LoggingSqliteContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingSqliteContext() : base()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<LoggingSqliteContext>();
            string connectionString = configuration.GetSqliteConnectionString(
                LoggingSqliteConstants.APPSETTING_DATABASE_CONNECTION);
            builder.UseSqlite(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(LoggingSqliteContext).Assembly.GetName().Name);
            });
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public LoggingSqliteContext(DbContextOptions<LoggingSqliteContext> options) : base(options)
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
            builder.HasDefaultSchema(LoggingSqliteConstants.DATABASE_SCHEMA_NAME);

            builder.Entity<LogMessage>().ToTable("LogMessage").HasKey(key => key.Key);

            builder.Entity<WebRequestMessage>().ToTable("WebRequestMessage").HasKey(key => key.Key);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .Properties<DateTimeOffset>()
                .HaveConversion<DateTimeOffsetToBytesConverter>();
            configurationBuilder
                .Properties<DateTimeOffset?>()
                .HaveConversion<DateTimeOffsetToBytesConverter>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().AddAppSettingsConfig().Build();
                string connectionString = configuration.GetSqlServerConnectionString(LoggingSqliteConstants.APPSETTING_DATABASE_CONNECTION);
                optionsBuilder.UseSqlite(connectionString, x =>
                {
                    x.MigrationsAssembly(typeof(LoggingSqliteContext).Assembly.GetName().Name);
                });
            }
        }
    }
}