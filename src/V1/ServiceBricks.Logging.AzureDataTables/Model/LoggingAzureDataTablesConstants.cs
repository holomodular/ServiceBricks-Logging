namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class LoggingAzureDataTablesConstants
    {
        public const string APPSETTINGS_CONNECTION_STRING = "ServiceBricks:Logging:AzureDataTables:ConnectionString";

        public const string TABLENAME_PREFIX = "Logging";

        public static string GetTableName(string tableName)
        {
            return TABLENAME_PREFIX + tableName;
        }
    }
}