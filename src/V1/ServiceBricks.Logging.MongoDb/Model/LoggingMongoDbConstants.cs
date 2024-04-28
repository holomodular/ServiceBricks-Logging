namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class LoggingMongoDbConstants
    {
        public const string APPSETTINGS_CONNECTION_STRING = "ServiceBricks:Logging:MongoDb:ConnectionString";
        public const string APPSETTINGS_DATABASE_NAME = "ServiceBricks:Logging:MongoDb:DatabaseName";

        public const string COLLECTIONNAME_PREFIX = "Logging";

        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}