using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Logging;
using ServiceQuery;

namespace ServiceBricks.Xunit
{
    public class WebRequestMessageTestManager : TestManager<WebRequestMessageDto>
    {
        public override WebRequestMessageDto GetMaximumDataObject()
        {
            var model = new WebRequestMessageDto()
            {
                CreateDate = DateTimeOffset.UtcNow,
                Application = Guid.NewGuid().ToString(),
                Server = Guid.NewGuid().ToString(),
                Exception = Guid.NewGuid().ToString(),
                RequestBody = Guid.NewGuid().ToString(),
                RequestContentLength = 1,
                RequestContentType = Guid.NewGuid().ToString(),
                RequestCookies = Guid.NewGuid().ToString(),
                RequestHasFormContentType = true,
                RequestHeaders = Guid.NewGuid().ToString(),
                RequestMethod = Guid.NewGuid().ToString(),
                RequestHost = Guid.NewGuid().ToString(),
                RequestIPAddress = Guid.NewGuid().ToString(),
                RequestIsHttps = true,
                RequestPath = Guid.NewGuid().ToString(),
                RequestPathBase = Guid.NewGuid().ToString(),
                RequestProtocol = Guid.NewGuid().ToString(),
                RequestQuery = Guid.NewGuid().ToString(),
                RequestQueryString = Guid.NewGuid().ToString(),
                RequestRouteValues = Guid.NewGuid().ToString(),
                RequestScheme = Guid.NewGuid().ToString(),
                UserStorageKey = Guid.NewGuid().ToString(),
                ResponseBody = Guid.NewGuid().ToString(),
                ResponseContentType = Guid.NewGuid().ToString(),
                ResponseCookies = Guid.NewGuid().ToString(),
                ResponseContentLength = 1,
                ResponseHeaders = Guid.NewGuid().ToString(),
                ResponseStatusCode = 1,
                ResponseTotalMilliseconds = 1,
            };
            return model;
        }

        public override IApiController<WebRequestMessageDto> GetController(IServiceProvider serviceProvider)
        {
            var options = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false, ExposeSystemErrors = true });
            return new WebRequestMessageApiController(serviceProvider.GetRequiredService<IWebRequestMessageApiService>(), options);
        }

        public override IApiController<WebRequestMessageDto> GetControllerReturnResponse(IServiceProvider serviceProvider)
        {
            var options = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = true, ExposeSystemErrors = true });
            return new WebRequestMessageApiController(serviceProvider.GetRequiredService<IWebRequestMessageApiService>(), options);
        }

        public override IApiClient<WebRequestMessageDto> GetClient(IServiceProvider serviceProvider)
        {
            var appconfig = serviceProvider.GetRequiredService<IConfiguration>();
            var config = new ConfigurationBuilder()
                .AddConfiguration(appconfig)
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "false" },
                    { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ExposeSystemErrors", "true" }
                })
                .Build();

            return new WebRequestMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                config);
        }

        public override IApiClient<WebRequestMessageDto> GetClientReturnResponse(IServiceProvider serviceProvider)
        {
            var appconfig = serviceProvider.GetRequiredService<IConfiguration>();
            var config = new ConfigurationBuilder()
                .AddConfiguration(appconfig)
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ReturnResponseObject", "true" },
                    { ServiceBricksConstants.APPSETTING_CLIENT_APIOPTIONS + ":ExposeSystemErrors", "true" }
                })
                .Build();

            return new WebRequestMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                config);
        }

        public override IApiService<WebRequestMessageDto> GetService(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IWebRequestMessageApiService>();
        }

        public override void UpdateObject(WebRequestMessageDto dto)
        {
            //dto.CreateDate = DateTimeOffset.UtcNow;
            dto.Application = Guid.NewGuid().ToString();
            dto.Server = Guid.NewGuid().ToString();
            dto.Exception = Guid.NewGuid().ToString();
            dto.RequestBody = Guid.NewGuid().ToString();
            dto.RequestContentLength = 2;
            dto.RequestContentType = Guid.NewGuid().ToString();
            dto.RequestCookies = Guid.NewGuid().ToString();
            dto.RequestHasFormContentType = true;
            dto.RequestHeaders = Guid.NewGuid().ToString();
            dto.RequestMethod = Guid.NewGuid().ToString();
            dto.RequestHost = Guid.NewGuid().ToString();
            dto.RequestIPAddress = Guid.NewGuid().ToString();
            dto.RequestIsHttps = true;
            dto.RequestPath = Guid.NewGuid().ToString();
            dto.RequestPathBase = Guid.NewGuid().ToString();
            dto.RequestProtocol = Guid.NewGuid().ToString();
            dto.RequestQuery = Guid.NewGuid().ToString();
            dto.RequestQueryString = Guid.NewGuid().ToString();
            dto.RequestRouteValues = Guid.NewGuid().ToString();
            dto.RequestScheme = Guid.NewGuid().ToString();
            dto.UserStorageKey = Guid.NewGuid().ToString();
            dto.ResponseBody = Guid.NewGuid().ToString();
            dto.ResponseContentType = Guid.NewGuid().ToString();
            dto.ResponseCookies = Guid.NewGuid().ToString();
            dto.ResponseContentLength = 2;
            dto.ResponseHeaders = Guid.NewGuid().ToString();
            dto.ResponseStatusCode = 2;
            dto.ResponseTotalMilliseconds = 2;
        }

        public override void ValidateObjects(WebRequestMessageDto clientDto, WebRequestMessageDto serviceDto, HttpMethod method)
        {
            //CreateDateRule
            if (method == HttpMethod.Post)
                Assert.True(serviceDto.CreateDate >= clientDto.CreateDate); //Rule
            else
                Assert.True(serviceDto.CreateDate == clientDto.CreateDate);

            Assert.True(serviceDto.Application == clientDto.Application);
            Assert.True(serviceDto.Server == clientDto.Server);
            Assert.True(serviceDto.Exception == clientDto.Exception);
            Assert.True(serviceDto.RequestBody == clientDto.RequestBody);
            Assert.True(serviceDto.RequestContentLength == clientDto.RequestContentLength);
            Assert.True(serviceDto.RequestContentType == clientDto.RequestContentType);
            Assert.True(serviceDto.RequestCookies == clientDto.RequestCookies);
            Assert.True(serviceDto.RequestHasFormContentType == clientDto.RequestHasFormContentType);
            Assert.True(serviceDto.RequestHeaders == clientDto.RequestHeaders);
            Assert.True(serviceDto.RequestMethod == clientDto.RequestMethod);
            Assert.True(serviceDto.RequestHost == clientDto.RequestHost);
            Assert.True(serviceDto.RequestIPAddress == clientDto.RequestIPAddress);
            Assert.True(serviceDto.RequestIsHttps == clientDto.RequestIsHttps);
            Assert.True(serviceDto.RequestPath == clientDto.RequestPath);
            Assert.True(serviceDto.RequestPathBase == clientDto.RequestPathBase);
            Assert.True(serviceDto.RequestProtocol == clientDto.RequestProtocol);
            Assert.True(serviceDto.RequestQuery == clientDto.RequestQuery);
            Assert.True(serviceDto.RequestQueryString == clientDto.RequestQueryString);
            Assert.True(serviceDto.RequestRouteValues == clientDto.RequestRouteValues);
            Assert.True(serviceDto.RequestScheme == clientDto.RequestScheme);
            Assert.True(serviceDto.UserStorageKey == clientDto.UserStorageKey);
            Assert.True(serviceDto.ResponseBody == clientDto.ResponseBody);
            Assert.True(serviceDto.ResponseContentType == clientDto.ResponseContentType);
            Assert.True(serviceDto.ResponseCookies == clientDto.ResponseCookies);
            Assert.True(serviceDto.ResponseContentLength == clientDto.ResponseContentLength);
            Assert.True(serviceDto.ResponseHeaders == clientDto.ResponseHeaders);
            Assert.True(serviceDto.ResponseStatusCode == clientDto.ResponseStatusCode);
            Assert.True(serviceDto.ResponseTotalMilliseconds == clientDto.ResponseTotalMilliseconds);

            //PrimaryKeyRule
            if (method == HttpMethod.Post)
                Assert.True(!string.IsNullOrEmpty(serviceDto.StorageKey));
            else
                Assert.True(serviceDto.StorageKey == clientDto.StorageKey);
        }

        public override List<ServiceQueryRequest> GetQueriesForObject(WebRequestMessageDto dto)
        {
            List<ServiceQueryRequest> queries = new List<ServiceQueryRequest>();

            var qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.CreateDate), dto.CreateDate.ToString("o"));
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.Application), dto.Application);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.Server), dto.Server);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.Exception), dto.Exception);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestBody), dto.RequestBody);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestContentLength), dto.RequestContentLength.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestContentType), dto.RequestContentType);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestCookies), dto.RequestCookies);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestHasFormContentType), dto.RequestHasFormContentType.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestHeaders), dto.RequestHeaders);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestMethod), dto.RequestMethod);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestHost), dto.RequestHost);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestIPAddress), dto.RequestIPAddress);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestIsHttps), dto.RequestIsHttps.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestPath), dto.RequestPath);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestPathBase), dto.RequestPathBase);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestProtocol), dto.RequestProtocol);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestQuery), dto.RequestQuery);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestQueryString), dto.RequestQueryString);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestRouteValues), dto.RequestRouteValues);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.RequestScheme), dto.RequestScheme);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.UserStorageKey), dto.UserStorageKey);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.ResponseBody), dto.ResponseBody);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.ResponseContentType), dto.ResponseContentType);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.ResponseCookies), dto.ResponseCookies);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.ResponseContentLength), dto.ResponseContentLength.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.ResponseHeaders), dto.ResponseHeaders);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.ResponseStatusCode), dto.ResponseStatusCode.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.ResponseTotalMilliseconds), dto.ResponseTotalMilliseconds.ToString());
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(WebRequestMessageDto.StorageKey), dto.StorageKey);
            queries.Add(qb.Build());

            return queries;
        }
    }
}