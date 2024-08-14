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
        public const string DEFAULT_CONTAINER_NAME = "Logging";
    }
}