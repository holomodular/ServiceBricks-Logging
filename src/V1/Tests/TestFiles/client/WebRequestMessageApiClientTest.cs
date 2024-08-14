using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;
using ServiceBricks.Logging.Client.Xunit;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class WebRequestMessageApiClientTest : ApiClientTest<WebRequestMessageDto>
    {
        public WebRequestMessageApiClientTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(ClientStartup));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<WebRequestMessageDto>>();
        }
    }
}