using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Logging;

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
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var newconfig = new Dictionary<string, string>()
            {
                ["ServiceBricks:Logging:Client:Api:BaseServiceUrl"] = "https://localhost:7000/api/v1.0",
            };
            var config = configurationBuilder
                .AddInMemoryCollection(newconfig).Build();
            var apioptions = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false });
            var apiservice = serviceProvider.GetRequiredService<IWebRequestMessageApiService>();
            var controller = new WebRequestMessageApiController(apiservice, apioptions);
            var options = new OptionsWrapper<ClientApiOptions>(new ClientApiOptions() { ReturnResponseObject = false, BaseServiceUrl = "https://localhost:7000/", TokenUrl = "https://localhost:7000/token" });
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
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            string myconfig = @"
{
  ""ServiceBricks"": {
    ""Client"": {
      ""Api"": {
        ""BaseServiceUrl"": ""https://localhost:7000/api/v1.0"",
        ""ReturnResponseObject"" : true
        }
    },
   }
}";
            var mem = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(myconfig));
            configurationBuilder.AddJsonStream(mem);

            var config = configurationBuilder.Build();

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

    public class StubWebRequestMessageApiClient : ApiClient<WebRequestMessageDto>, IWebRequestMessageApiClient
    {
        public StubWebRequestMessageApiClient(
            ILoggerFactory loggerFactory,
            IHttpClientFactory httpClientFactory,
            IOptions<ClientApiOptions> clientApiOptions)
            : base(loggerFactory, httpClientFactory, clientApiOptions.Value)
        {
            ApiResource = $"Logging/WebRequestMessage";
        }
    }
}