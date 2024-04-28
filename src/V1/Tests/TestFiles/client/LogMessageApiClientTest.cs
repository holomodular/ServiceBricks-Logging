using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Xunit;
using ServiceBricks.Logging;
using ServiceBricks.Logging.Client.Xunit;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class LogMessageApiClientTest : ApiClientTest<LogMessageDto>
    {
        public LogMessageApiClientTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(ClientStartup));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<LogMessageDto>>();
        }
    }
}