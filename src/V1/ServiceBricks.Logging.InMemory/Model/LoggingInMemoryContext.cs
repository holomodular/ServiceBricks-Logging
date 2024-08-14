using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    /// <summary>
    /// The database context for the Logging InMemory module.
    /// </summary>
    public partial class LoggingInMemoryContext : DbContext
    {
        protected DbContextOptions<LoggingInMemoryContext> _options = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingInMemoryContext() : base()
        {
            var configBuider = new ConfigurationBuilder();
            configBuider.AddAppSettingsConfig();
            var configuration = configBuider.Build();

            var builder = new DbContextOptionsBuilder<LoggingInMemoryContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = builder.Options;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public LoggingInMemoryContext(DbContextOptions<LoggingInMemoryContext> options) : base(options)
        {
            _options = options;
        }

        /// <summary>
        /// The LogMessages DbSet.
        /// </summary>
        public virtual DbSet<LogMessage> LogMessages { get; set; }

        /// <summary>
        /// The WebRequestMessages DbSet.
        /// </summary>
        public virtual DbSet<WebRequestMessage> WebRequestMessages { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // AI: Setup the entities to the model
            builder.Entity<LogMessage>().HasKey(key => key.Key);

            builder.Entity<WebRequestMessage>().HasKey(key => key.Key);
        }

        /// <summary>
        /// Create context.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual LoggingInMemoryContext CreateDbContext(string[] args)
        {
            return new LoggingInMemoryContext(_options);
        }
    }
}