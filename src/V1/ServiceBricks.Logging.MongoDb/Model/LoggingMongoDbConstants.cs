namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// These are constants for the ServiceBricks Logging MongoDb module.
    /// </summary>
    public static partial class LoggingMongoDbConstants
    {
        /// <summary>
        /// Application setting for the connection string.
        /// </summary>
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:MongoDb:ConnectionString";

        /// <summary>
        /// Application setting for the database name.
        /// </summary>
        public const string APPSETTINGS_DATABASE = "ServiceBricks:Logging:Storage:MongoDb:Database";

        /// <summary>
        /// The prefix for the collection name.
        /// </summary>
        public const string COLLECTIONNAME_PREFIX = "Logging";

        /// <summary>
        /// Get the collection name for the given table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}