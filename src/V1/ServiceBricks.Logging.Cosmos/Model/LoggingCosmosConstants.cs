namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class LoggingCosmosConstants
    {
        public const string APPSETTING_CONNECTION_STRING = "ServiceBricks:Logging:Storage:Cosmos:ConnectionString";
        public const string APPSETTING_DATABASE = "ServiceBricks:Logging:Storage:Cosmos:Database";

        public const string DEFAULT_CONTAINER_NAME = "Logging";
    }
}