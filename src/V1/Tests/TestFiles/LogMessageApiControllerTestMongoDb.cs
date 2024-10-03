using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class LogMessageApiControllerTestMongoDb : LogMessageApiControllerTest
    {
        public LogMessageApiControllerTestMongoDb()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupMongoDb));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<LogMessageDto>>();
        }
    }
}