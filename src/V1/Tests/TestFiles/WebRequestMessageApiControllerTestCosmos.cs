using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class WebRequestMessageApiControllerTestCosmos : WebRequestMessageApiControllerTest
    {
        public WebRequestMessageApiControllerTestCosmos()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupCosmos));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<WebRequestMessageDto>>();
        }
    }
}