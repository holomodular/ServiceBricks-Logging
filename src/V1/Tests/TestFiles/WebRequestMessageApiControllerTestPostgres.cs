﻿using Microsoft.Extensions.DependencyInjection;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit.Integration
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class WebRequestMessageApiControllerTestPostgres : WebRequestMessageApiControllerTest
    {
        public WebRequestMessageApiControllerTestPostgres()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupPostgres));
            TestManager = SystemManager.ServiceProvider.GetRequiredService<ITestManager<WebRequestMessageDto>>();
        }
    }
}