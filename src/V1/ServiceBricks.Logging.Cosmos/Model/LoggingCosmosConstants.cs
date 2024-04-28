namespace ServiceBricks.Logging.Cosmos
{
    /// <summary>
    /// This is constants for the Log module.
    /// </summary>
    public static class LoggingCosmosConstants
    {
        public const string APPSETTING_CONNECTION = "ServiceBricks:Logging:Cosmos:ConnectionString";
        public const string APPSETTING_DATABASE = "ServiceBricks:Logging:Cosmos:Database";

        public const string DEFAULT_CONTAINER_NAME = "Logging";
    }
}