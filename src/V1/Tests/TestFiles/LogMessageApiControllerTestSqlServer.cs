using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class LogMessageApiControllerTestSqlServer : LogMessageApiControllerTest
    {
        public LogMessageApiControllerTestSqlServer()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupSqlServer));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<LogMessageDto>>();
        }
    }
}