namespace ServiceBricks.Logging.Sqlite
{
    /// <summary>
    /// These are constants for the ServiceBricks Logging Sqlite module.
    /// </summary>
    public static partial class LoggingSqliteConstants
    {
        /// <summary>
        /// Application setting key for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:Sqlite:ConnectionString";

        /// <summary>
        /// Application setting key for the schema name.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Logging";
    }
}