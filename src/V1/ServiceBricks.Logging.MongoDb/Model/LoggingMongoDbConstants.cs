namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class LoggingMongoDbConstants
    {
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:MongoDb:ConnectionString";
        public const string APPSETTINGS_DATABASE = "ServiceBricks:Logging:Storage:MongoDb:Database";

        public const string COLLECTIONNAME_PREFIX = "Logging";

        public static string GetCollectionName(string tableName)
        {
            return COLLECTIONNAME_PREFIX + tableName;
        }
    }
}