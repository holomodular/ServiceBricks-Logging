using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using ServiceBricks.Xunit;
using ServiceBricks.Logging.Client.Xunit;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class WebRequestMessageApiClientReturnResponseTest : ApiClientReturnResponseTest<WebRequestMessageDto>
    {
        public WebRequestMessageApiClientReturnResponseTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(ClientStartup));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<WebRequestMessageDto>>();
        }
    }
}