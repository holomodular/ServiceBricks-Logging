using ServiceBricks.Logging;

namespace ServiceBricks.Xunit
{
    public class WebRequestMessageTestManagerMongoDb : WebRequestMessageTestManager
    {
        public override WebRequestMessageDto GetObjectNotFound()
        {
            return new WebRequestMessageDto()
            {
                StorageKey = "000000000000000000000000"
            };
        }
    }
}