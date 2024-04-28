using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Logging;
using ServiceQuery;

namespace ServiceBricks.Xunit
{
    public class LogMessageTestManager : TestManager<LogMessageDto>
    {
        public override LogMessageDto GetMaximumDataObject()
        {
            var model = new LogMessageDto()
            {
                Application = Guid.NewGuid().ToString(),
                Category = Guid.NewGuid().ToString(),
                Server = Guid.NewGuid().ToString(),
                CreateDate = DateTimeOffset.UtcNow,
                Exception = Guid.NewGuid().ToString(),
                Level = Guid.NewGuid().ToString(),
                Message = Guid.NewGuid().ToString(),
                Path = Guid.NewGuid().ToString(),
                Properties = Guid.NewGuid().ToString(),
                UserStorageKey = Guid.NewGuid().ToString(),
            };
            return model;
        }

        public override IApiController<LogMessageDto> GetController(IServiceProvider serviceProvider)
        {
            var options = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = false, ExposeSystemErrors = true });
            return new LogMessageApiController(serviceProvider.GetRequiredService<ILogMessageApiService>(), options);
        }

        public override IApiController<LogMessageDto> GetControllerReturnResponse(IServiceProvider serviceProvider)
        {
            var options = new OptionsWrapper<ApiOptions>(new ApiOptions() { ReturnResponseObject = true, ExposeSystemErrors = true });
            return new LogMessageApiController(serviceProvider.GetRequiredService<ILogMessageApiService>(), options);
        }

        public override IApiClient<LogMessageDto> GetClient(IServiceProvider serviceProvider)
        {
            return new LogMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                serviceProvider.GetRequiredService<IConfiguration>());
        }

        public override IApiClient<LogMessageDto> GetClientReturnResponse(IServiceProvider serviceProvider)
        {
            return new LogMessageApiClient(
                serviceProvider.GetRequiredService<ILoggerFactory>(),
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                serviceProvider.GetRequiredService<IConfiguration>());
        }

        public override IApiService<LogMessageDto> GetService(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ILogMessageApiService>();
        }

        public override void UpdateObject(LogMessageDto dto)
        {
            dto.Application = Guid.NewGuid().ToString();
            dto.Category = Guid.NewGuid().ToString();
            dto.Server = Guid.NewGuid().ToString();
            dto.Exception = Guid.NewGuid().ToString();
            dto.Level = Guid.NewGuid().ToString();
            dto.Message = Guid.NewGuid().ToString();
            dto.Path = Guid.NewGuid().ToString();
            dto.Properties = Guid.NewGuid().ToString();
            dto.UserStorageKey = Guid.NewGuid().ToString();
        }

        public override void ValidateObjects(LogMessageDto clientDto, LogMessageDto serviceDto, HttpMethod method)
        {
            //CreateDateRule
            if (method == HttpMethod.Post)
                Assert.True(serviceDto.CreateDate >= clientDto.CreateDate); //Rule
            else
                Assert.True(serviceDto.CreateDate == clientDto.CreateDate);

            Assert.True(serviceDto.Application == clientDto.Application);

            Assert.True(serviceDto.Category == clientDto.Category);

            Assert.True(serviceDto.Server == clientDto.Server);

            Assert.True(serviceDto.Exception == clientDto.Exception);

            //PrimaryKeyRule
            if (method == HttpMethod.Post)
                Assert.True(!string.IsNullOrEmpty(serviceDto.StorageKey));
            else
                Assert.True(serviceDto.StorageKey == clientDto.StorageKey);

            Assert.True(serviceDto.Level == clientDto.Level);

            Assert.True(serviceDto.Message == clientDto.Message);

            Assert.True(serviceDto.Path == clientDto.Path);

            Assert.True(serviceDto.Properties == clientDto.Properties);

            Assert.True(serviceDto.UserStorageKey == clientDto.UserStorageKey);
        }

        public override List<ServiceQueryRequest> GetQueriesForObject(LogMessageDto dto)
        {
            List<ServiceQueryRequest> queries = new List<ServiceQueryRequest>();

            var qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.Application), dto.Application);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.Category), dto.Category);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.CreateDate), dto.CreateDate.ToString("o"));
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.Exception), dto.Exception);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.Level), dto.Level);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.Message), dto.Message);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.Path), dto.Path);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.Properties), dto.Properties);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.Server), dto.Server);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.StorageKey), dto.StorageKey);
            queries.Add(qb.Build());

            qb = ServiceQueryRequestBuilder.New().
                IsEqual(nameof(LogMessageDto.UserStorageKey), dto.UserStorageKey);
            queries.Add(qb.Build());

            return queries;
        }
    }
}