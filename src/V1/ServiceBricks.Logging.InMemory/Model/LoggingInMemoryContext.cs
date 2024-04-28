using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceBricks.Logging.EntityFrameworkCore;

namespace ServiceBricks.Logging.InMemory
{
    // dotnet ef migrations add LoggingV1 --context LoggingContext --startup-project ../../Web/ServiceBrick.WebApp

    /// <summary>
    /// The database context for Logging.
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

        public virtual DbSet<LogMessage> LogMessages { get; set; }

        public virtual DbSet<WebRequestMessage> WebRequestMessages { get; set; }

        /// <summary>
        /// OnModelCreating.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LogMessage>().HasKey(key => key.Key);

            builder.Entity<WebRequestMessage>().HasKey(key => key.Key);
        }
    }
}