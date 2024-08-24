using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Logging.EntityFrameworkCore;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.SqlServer
{
    // dotnet ef migrations add LoggingV1 --context LoggingSqlServerContext --startup-project ../Tests/MigrationsHost

    /// <summary>
    /// The database context for the ServiceBricks.Logging.SqlServer module.
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
                LoggingSqlServerConstants.APPSETTING_CONNECTION_STRING);
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

        /// <summary>
        /// Log Messages.
        /// </summary>
        public virtual DbSet<LogMessage> LogMessages { get; set; }

        /// <summary>
        /// Web Request Messages.
        /// </summary>
        public virtual DbSet<WebRequestMessage> WebRequestMessages { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // AI: Set the default schema
            builder.HasDefaultSchema(LoggingSqlServerConstants.DATABASE_SCHEMA_NAME);

            // AI: Setup the entities to the model
            builder.Entity<LogMessage>().HasKey(key => key.Key);
            builder.Entity<LogMessage>().HasIndex(key => new { key.Application, key.Level, key.CreateDate });

            builder.Entity<WebRequestMessage>().HasKey(key => key.Key);
            builder.Entity<WebRequestMessage>().HasIndex(key => new { key.Application, key.UserStorageKey, key.CreateDate });
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual LoggingSqlServerContext CreateDbContext(string[] args)
        {
            return new LoggingSqlServerContext(_options);
        }
    }
}