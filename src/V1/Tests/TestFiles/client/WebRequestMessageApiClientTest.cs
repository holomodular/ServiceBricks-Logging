using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Xunit;
using ServiceBricks.Logging.Client.Xunit;
using ServiceBricks.Logging;

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