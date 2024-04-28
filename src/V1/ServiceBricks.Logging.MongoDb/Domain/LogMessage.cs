using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ServiceBricks.Storage.MongoDb;
using System.Linq.Expressions;

namespace ServiceBricks.Logging.MongoDb
{
    /// <summary>
    /// This is an application log message.
    /// </summary>
    public class LogMessage : MongoDbDomainObject<LogMessage>, IDpCreateDate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Key { get; set; }

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

        public override Expression<Func<LogMessage, bool>> DomainGetItemFilter(LogMessage obj)
        {
            return x => x.Key == obj.Key;
        }
    }
}