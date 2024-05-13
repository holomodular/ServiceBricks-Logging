namespace ServiceBricks.Logging.Postgres
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class LoggingPostgresConstants
    {
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:Postgres:ConnectionString";

        public const string DATABASE_SCHEMA_NAME = "Logging";
    }
}