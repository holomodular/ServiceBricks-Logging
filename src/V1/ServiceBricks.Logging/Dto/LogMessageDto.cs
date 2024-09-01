namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a log message data transer object.
    /// </summary>
    public partial class LogMessageDto : DataTransferObject
    {
        /// <summary>
        /// The date and time the log message was created in UTC.
        /// </summary>
        public virtual DateTimeOffset CreateDate { get; set; }

        /// <summary>
        /// The name of the application that created the log message.
        /// </summary>
        public virtual string Application { get; set; }

        /// <summary>
        /// The name of the server that created the log message.
        /// </summary>
        public virtual string Server { get; set; }

        /// <summary>
        /// The category name that the log message belongs to.
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// The user storage key associated to the log message.
        /// </summary>
        public virtual string UserStorageKey { get; set; }

        /// <summary>
        /// The URI path of the request of the log message.
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// The severity level of the the log message.
        /// </summary>
        public virtual string Level { get; set; }

        /// <summary>
        /// The message of the log message.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// The exception that was thrown.
        /// </summary>
        public virtual string Exception { get; set; }

        /// <summary>
        /// Additional properties of the log message.
        /// </summary>
        public virtual string Properties { get; set; }
    }
}