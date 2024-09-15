using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Xunit;
using Newtonsoft.Json;
using ServiceQuery;
using Microsoft.AspNetCore.Mvc;
using static ServiceBricks.Xunit.ApiClientTests;
using ServiceBricks.Logging;
using Microsoft.Extensions.Configuration;

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
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var newconfig = new Dictionary<string, string>()
            {
                ["ServiceBricks:Logging:Client:Api:BaseServiceUrl"] = "https://localhost:7000/api/v1.0",
            };
            var config = configurationBuilder
                .AddInMemoryCollection(newconfig).Build();
            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false });
            var apiservice = serviceProvider.GetRequiredService<ILogMessageApiService>();
            var controller = new LogMessageApiController(apiservice, apioptions);
            var options = new OptionsWrapper<ClientApiOptions>(new ClientApiOptions() { ReturnResponseObject = false, BaseServiceUrl = "https://localhost:7000/", TokenUrl = "https://localhost:7000/token" });
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
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var newconfig = new Dictionary<string, string>()
            {
                ["ServiceBricks:Logging:Client:Api:BaseServiceUrl"] = "https://localhost:7000/api/v1.0",
                ["ServiceBricks:Logging:Client:Api:ReturnResponseObject"] = "true",
            };
            var config = configurationBuilder
                .AddInMemoryCollection(newconfig).Build();

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