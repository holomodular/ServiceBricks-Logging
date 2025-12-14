using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Logging;
using System.Collections.Generic;

namespace ServiceBricks.Xunit
{
    public class WebRequestMessageStubTestManager : WebRequestMessageTestManager
    {
        public class WebRequestMessageHttpClientFactory : IHttpClientFactory
        {
            private ApiClientTests.CustomGenericHttpClientHandler<WebRequestMessageDto> _handler;

            public WebRequestMessageHttpClientFactory(ApiClientTests.CustomGenericHttpClientHandler<WebRequestMessageDto> handler)
            {
                _handler = handler;
            }

            public HttpClient CreateClient(string name)
            {
                return new HttpClient(_handler);
            }
        }

        public override IApiClient<WebRequestMessageDto> GetClient(IServiceProvider serviceProvider)
        {
            var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":DisableAuthentication", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":TokenUrl", "https://localhost:7000/token" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":BaseServiceUrl", "https://localhost:7000/" },
            })
            .Build();

            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false });
            var apiservice = serviceProvider.GetRequiredService<IWebRequestMessageApiService>();
            var controller = new WebRequestMessageApiController(apiservice, apioptions);            
            var handler = new ApiClientTests.CustomGenericHttpClientHandler<WebRequestMessageDto>(controller);
            var clientHandlerFactory = new WebRequestMessageHttpClientFactory(handler);
            return new WebRequestMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                clientHandlerFactory,
                config);
        }

        public ApiClientTests.CustomGenericHttpClientHandler<WebRequestMessageDto> Handler { get; set; }

        public override IApiClient<WebRequestMessageDto> GetClientReturnResponse(IServiceProvider serviceProvider)
        {
            var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "true" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":DisableAuthentication", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":TokenUrl", "https://localhost:7000/token" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":BaseServiceUrl", "https://localhost:7000/" },
            })
            .Build();

            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = true });
            var apiservice = serviceProvider.GetRequiredService<IWebRequestMessageApiService>();
            var controller = new WebRequestMessageApiController(apiservice, apioptions);
            var handler = new ApiClientTests.CustomGenericHttpClientHandler<WebRequestMessageDto>(controller);
            var clientHandlerFactory = new WebRequestMessageHttpClientFactory(handler);
            return new WebRequestMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                clientHandlerFactory,
                config);
        }
    }

    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class StubWebRequestMessageApiClientTest : ApiClientTest<WebRequestMessageDto>
    {
        public StubWebRequestMessageApiClientTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = new WebRequestMessageStubTestManager();
        }
    }

    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class StubWebRequestMessageApiClientReturnResponseTests : ApiClientReturnResponseTest<WebRequestMessageDto>
    {
        public StubWebRequestMessageApiClientReturnResponseTests()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = new WebRequestMessageStubTestManager();
        }
    }

}