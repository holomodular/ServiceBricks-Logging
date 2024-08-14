using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using ServiceBricks.Logging;
using ServiceBricks.Logging.Client.Xunit;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class LogMessageApiClientReturnResponseTest : ApiClientReturnResponseTest<LogMessageDto>
    {
        public LogMessageApiClientReturnResponseTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(ClientStartup));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<LogMessageDto>>();
        }
    }
}