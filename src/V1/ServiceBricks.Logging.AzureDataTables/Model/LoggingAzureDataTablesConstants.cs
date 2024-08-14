namespace ServiceBricks.Logging.AzureDataTables
{
    /// <summary>
    /// This is constants for the ServiceBricks Logging Azure Data Tables module.
    /// </summary>
    public static partial class LoggingAzureDataTablesConstants
    {
        /// <summary>
        /// AppSetting key for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:AzureDataTables:ConnectionString";

        /// <summary>
        /// Table name prefix for the module.
        /// </summary>
        public const string TABLENAME_PREFIX = "Logging";

        /// <summary>
        /// Get a table name for the module.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetTableName(string tableName)
        {
            return TABLENAME_PREFIX + tableName;
        }
    }
}