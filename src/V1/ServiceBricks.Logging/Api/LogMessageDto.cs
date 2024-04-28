namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a log message data transer object.
    /// </summary>
    public class LogMessageDto : DataTransferObject
    {
        public virtual DateTimeOffset CreateDate { get; set; }
        public virtual string Application { get; set; }
        public virtual string Server { get; set; }
        public virtual string Category { get; set; }
        public virtual string UserStorageKey { get; set; }
        public virtual string Path { get; set; }
        public virtual string Level { get; set; }
        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }
        public virtual string Properties { get; set; }
    }
}