namespace ServiceBricks.Logging.Postgres
{
    /// <summary>
    /// These are constants for the ServiceBricks Logging Postgres module.
    /// </summary>
    public static partial class LoggingPostgresConstants
    {
        /// <summary>
        /// Application setting key for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:Postgres:ConnectionString";

        /// <summary>
        /// The default schema name.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Logging";
    }
}