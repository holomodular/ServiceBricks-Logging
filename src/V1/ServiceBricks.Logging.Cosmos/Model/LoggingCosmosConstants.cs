namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// These are constants for the ServiceBricks Logging Cosmos module.
    /// </summary>
    public static partial class LoggingCosmosConstants
    {
        /// <summary>
        /// Application Setting for the Cosmos Connection String.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:Cosmos:ConnectionString";

        /// <summary>
        /// Application Setting for the Cosmos Database.
        /// </summary>
        public const string APPSETTING_DATABASE = "ServiceBricks:Logging:Storage:Cosmos:Database";

        /// <summary>
        /// Default Container Name.
        /// </summary>
        public const string CONTAINER_PREFIX = "Logging";

        /// <summary>
        /// Get the container name for the given table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetContainerName(string tableName)
        {
            return CONTAINER_PREFIX + tableName;
        }
    }
}