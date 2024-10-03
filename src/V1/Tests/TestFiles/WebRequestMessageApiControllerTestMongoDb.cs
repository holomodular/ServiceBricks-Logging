using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class WebRequestMessageApiControllerTestMongoDb : WebRequestMessageApiControllerTest
    {
        public WebRequestMessageApiControllerTestMongoDb()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupMongoDb));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<WebRequestMessageDto>>();
        }
    }
}