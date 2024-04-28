using ServiceBricks.Logging;

namespace ServiceBricks.Xunit
{
    public class LogMessageTestManagerMongoDb : LogMessageTestManager
    {
        public override LogMessageDto GetObjectNotFound()
        {
            return new LogMessageDto()
            {
                StorageKey = "000000000000000000000000"
            };
        }
    }
}