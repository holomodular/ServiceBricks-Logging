using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Logging;

namespace ServiceBricks.Xunit
{
    public class LogMessageStubTestManager : LogMessageTestManager
    {
        public class LogMessageHttpClientFactory : IHttpClientFactory
        {
            private ApiClientTests.CustomGenericHttpClientHandler<LogMessageDto> _handler;

            public LogMessageHttpClientFactory(ApiClientTests.CustomGenericHttpClientHandler<LogMessageDto> handler)
            {
                _handler = handler;
            }

            public HttpClient CreateClient(string name)
            {
                return new HttpClient(_handler);
            }
        }

        public override IApiClient<LogMessageDto> GetClient(IServiceProvider serviceProvider)
        {
            var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":DisableAuthentication", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":TokenUrl", "https://localhost:7000/token" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":BaseServiceUrl", "https://localhost:7000/" },
            })
            .Build();

            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false });
            var apiservice = serviceProvider.GetRequiredService<ILogMessageApiService>();
            var controller = new LogMessageApiController(apiservice, apioptions);
            var handler = new ApiClientTests.CustomGenericHttpClientHandler<LogMessageDto>(controller);
            var clientHandlerFactory = new LogMessageHttpClientFactory(handler);
            return new LogMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                clientHandlerFactory,
                config);
        }

        public ApiClientTests.CustomGenericHttpClientHandler<LogMessageDto> Handler { get; set; }

        public override IApiClient<LogMessageDto> GetClientReturnResponse(IServiceProvider serviceProvider)
        {
            var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "true" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":DisableAuthentication", "false" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":TokenUrl", "https://localhost:7000/token" },
                            { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":BaseServiceUrl", "https://localhost:7000/" },
            })
            .Build();

            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = true });
            var apiservice = serviceProvider.GetRequiredService<ILogMessageApiService>();
            var controller = new LogMessageApiController(apiservice, apioptions);
            var handler = new ApiClientTests.CustomGenericHttpClientHandler<LogMessageDto>(controller);
            var clientHandlerFactory = new LogMessageHttpClientFactory(handler);
            return new LogMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                clientHandlerFactory,
                config);
        }
    }

    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class StubLogMessageApiClientTest : ApiClientTest<LogMessageDto>
    {
        public StubLogMessageApiClientTest()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = new LogMessageStubTestManager();
        }
    }

    [Collection(ServiceBricks.Xunit.Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class StubLogMessageApiClientReturnResponseTests : ApiClientReturnResponseTest<LogMessageDto>
    {
        public StubLogMessageApiClientReturnResponseTests()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
            TestManager = new LogMessageStubTestManager();
        }
    }
}