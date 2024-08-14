namespace ServiceBricks.Logging.SqlServer
{
    /// <summary>
    /// These are constants for the ServiceBricks.Logging.SqlServer module.
    /// </summary>
    public static partial class LoggingSqlServerConstants
    {
        /// <summary>
        /// AppSetting key for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:SqlServer:ConnectionString";

        /// <summary>
        /// The name of the database schema.
        /// </summary>
        public const string DATABASE_SCHEMA_NAME = "Logging";
    }
}