using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Sqlite
{
    // dotnet ef migrations add LoggingV1 --context LoggingSqliteContext --startup-project ../Tests/MigrationsHost

    /// <summary>
    /// The database context for the ServiceBricks.Logging.Sqlite module.
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
                LoggingSqliteConstants.APPSETTING_CONNECTION_STRING);
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

        /// <summary>
        /// Log Messages.
        /// </summary>
        public virtual DbSet<LogMessage> LogMessage { get; set; }

        /// <summary>
        /// Web Request Messages.
        /// </summary>
        public virtual DbSet<WebRequestMessage> WebRequestMessage { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // AI: Setup the entities to the model
            builder.Entity<LogMessage>().HasKey(key => key.Key);
            builder.Entity<LogMessage>().HasIndex(key => new { key.Application, key.Level, key.CreateDate });

            builder.Entity<WebRequestMessage>().HasKey(key => key.Key);
            builder.Entity<WebRequestMessage>().HasIndex(key => new { key.Application, key.UserStorageKey, key.CreateDate });
        }

        /// <summary>
        /// ConfigureConventions.
        /// </summary>
        /// <param name="configurationBuilder"></param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<DateTimeOffset>()
                .HaveConversion<DateTimeOffsetToBytesConverter>();
            configurationBuilder
                .Properties<DateTimeOffset?>()
                .HaveConversion<DateTimeOffsetToBytesConverter>();
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual LoggingSqliteContext CreateDbContext(string[] args)
        {
            return new LoggingSqliteContext(_options);
        }
    }
}