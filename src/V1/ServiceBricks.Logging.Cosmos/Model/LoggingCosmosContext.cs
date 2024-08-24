using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Storage.EntityFrameworkCore;

namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// The database context for Logging.
    /// </summary>
    public partial class LoggingCosmosContext : DbContext
    {
        protected DbContextOptions<LoggingCosmosContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingCosmosContext() : base()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<LoggingCosmosContext>();
            string connectionString = configuration.GetCosmosConnectionString(LoggingCosmosConstants.APPSETTING_CONNECTION_STRING);
            string database = configuration.GetCosmosDatabase(LoggingCosmosConstants.APPSETTING_DATABASE);
            builder.UseCosmos(connectionString, database);

            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public LoggingCosmosContext(DbContextOptions<LoggingCosmosContext> options) : base(options)
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

            // AI: Create the model for each table
            builder.Entity<LogMessage>().HasKey(p => p.Key);
            builder.Entity<LogMessage>().HasPartitionKey(p => p.PartitionKey);
            builder.Entity<LogMessage>().HasIndex(key => new { key.Application, key.Level, key.CreateDate });
            builder.Entity<LogMessage>().ToContainer(LoggingCosmosConstants.GetContainerName(nameof(LogMessage)));

            builder.Entity<WebRequestMessage>().HasKey(p => p.Key);
            builder.Entity<WebRequestMessage>().HasPartitionKey(p => p.PartitionKey);
            builder.Entity<WebRequestMessage>().HasIndex(key => new { key.Application, key.UserStorageKey, key.CreateDate });
            builder.Entity<WebRequestMessage>().ToContainer(LoggingCosmosConstants.GetContainerName(nameof(WebRequestMessage)));
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual LoggingCosmosContext CreateDbContext(string[] args)
        {
            return new LoggingCosmosContext(_options);
        }
    }
}