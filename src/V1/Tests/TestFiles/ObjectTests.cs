using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Logging;
using ServiceQuery;
using static ServiceBricks.Xunit.BusinessRuleTests;

namespace ServiceBricks.Xunit
{
    [Collection(Constants.SERVICEBRICKS_COLLECTION_NAME)]
    public class ObjectTests
    {
        public virtual ISystemManager SystemManager { get; set; }

        public ObjectTests()
        {
            SystemManager = ServiceBricksSystemManager.GetSystemManager(typeof(StartupInMemory));
        }

        [Fact]
        public virtual async Task CustomLoggerWriteMessageTaskTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            CustomLogger.MessageQueue.Enqueue(new CustomLoggerMessage()
            {
                Message = "test"
            });

            CustomLoggerWriteMessageTask.QueueWork(taskQueue);
            CustomLoggerWriteMessageTask.Worker worker = new CustomLoggerWriteMessageTask.Worker(
                loggerFactory,
                SystemManager.ServiceProvider.GetRequiredService<ILogMessageApiService>());

            await worker.DoWork(new CustomLoggerWriteMessageTask.Detail(), CancellationToken.None);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(3000);
            while (CustomLogger.MessageQueue.Count != 0)
            {
                if (cts.Token.IsCancellationRequested)
                    break;
            }

            Assert.True(CustomLogger.MessageQueue.Count == 0);
        }

        [Fact]
        public virtual async Task CustomLoggerWriteMessageTimerTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            CustomLogger.MessageQueue.Enqueue(new CustomLoggerMessage()
            {
                Message = "test"
            });

            var timer = new CustomLoggerWriteMessageTimer(
                SystemManager.ServiceProvider,
                SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>());

            CustomLogger.MessageQueue.Enqueue(new CustomLoggerMessage()
            {
                Message = "test"
            });

            await timer.StartAsync(CancellationToken.None);

            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(3000);
            while (CustomLogger.MessageQueue.Count != 0)
            {
                if (cts.Token.IsCancellationRequested)
                    break;
            }

            Assert.True(CustomLogger.MessageQueue.Count == 0);
        }

        [Fact]
        public virtual Task CustomLoggerTests()
        {
            var loggerFactory = SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var taskQueue = SystemManager.ServiceProvider.GetRequiredService<ITaskQueue>();

            CustomLogger.MessageQueue.Enqueue(new CustomLoggerMessage()
            {
                Message = "test"
            });

            IExternalScopeProvider extsp = new LoggerExternalScopeProvider();
            extsp.Push(new KeyValuePair<string, object>(LoggingConstants.MiddlewareStateProperty.USER_STORAGE_KEY, Guid.NewGuid().ToString()));
            extsp.Push(new KeyValuePair<string, object>(LoggingConstants.MiddlewareStateProperty.PATH, Guid.NewGuid().ToString()));
            extsp.Push(new KeyValuePair<string, object>(LoggingConstants.MiddlewareStateProperty.IPADDRESS, Guid.NewGuid().ToString()));
            CustomLogger logger = new CustomLogger("test", "test", extsp);
            Assert.True(logger != null);

            var enabled = logger.IsEnabled(LogLevel.Information);
            Assert.True(enabled);

            logger.Log(LogLevel.Information, 0, "test", null, (s, e) => s);

            return Task.CompletedTask;
        }

        [Fact]
        public virtual Task AddLoggingTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();

            var config = new ConfigurationBuilder().Build();

            services.AddServiceBricks(config);
            services.AddServiceBricksLogging(config);

            return Task.CompletedTask;
        }

        [Fact]
        public virtual Task CustomLoggerProviderTests()
        {
            var apioptions = new OptionsWrapper<ApplicationOptions>(new ApplicationOptions() { Name = "test" });

            CustomLoggerProvider provider = new CustomLoggerProvider(apioptions);
            var logger = provider.CreateLogger("test");

            IExternalScopeProvider extsp = new LoggerExternalScopeProvider();
            extsp.Push(new KeyValuePair<string, object>(LoggingConstants.MiddlewareStateProperty.USER_STORAGE_KEY, Guid.NewGuid().ToString()));
            extsp.Push(new KeyValuePair<string, object>(LoggingConstants.MiddlewareStateProperty.PATH, Guid.NewGuid().ToString()));

            provider.SetScopeProvider(extsp);

            return Task.CompletedTask;
        }

        [Fact]
        public virtual async Task LogMessageMiddlewareTests()
        {
            var testHttpContextAccessor = new MiddlewareTests.TestHttpContextAccessor();
            testHttpContextAccessor.HttpContext.Request.Path = "/api/test";

            LogMessageMiddleware middleware = new LogMessageMiddleware(
                (innerHttpContext) =>
                {
                    return Task.CompletedTask;
                },
            SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>());

            await middleware.InvokeAsync(testHttpContextAccessor.HttpContext);
        }

        [Fact]
        public virtual async Task WebRequestMessageMiddlewareTests()
        {
            var testHttpContextAccessor = new MiddlewareTests.TestHttpContextAccessor();
            testHttpContextAccessor.HttpContext.Request.Path = "/api/test";
            var appoptions = new OptionsWrapper<ApplicationOptions>(new ApplicationOptions() { Name = "test" });
            var wrmoptions = new OptionsWrapper<WebRequestMessageOptions>(new WebRequestMessageOptions() { EnableLogging = true });
            WebRequestMessageMiddleware middleware = new WebRequestMessageMiddleware(
                SystemManager.ServiceProvider.GetRequiredService<IWebRequestMessageApiService>(),
                wrmoptions,
                appoptions);

            await middleware.InvokeAsync(
                testHttpContextAccessor.HttpContext,
               (innerHttpContext) =>
               {
                   return Task.CompletedTask;
               });
        }

        [Fact]
        public virtual Task CreateApplicationLogRuleTests()
        {
            CreateApplicationLogRule rule = new CreateApplicationLogRule(
                SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>(),
                SystemManager.ServiceProvider.GetRequiredService<ILogMessageApiService>(),
                SystemManager.ServiceProvider.GetRequiredService<IMapper>());

            BusinessRuleContext context = new BusinessRuleContext();
            context.Object = new CreateApplicationLogBroadcast(new ApplicationLogDto()
            {
            });

            rule.ExecuteRule(context);

            return Task.CompletedTask;
        }

        [Fact]
        public virtual async Task CreateApplicationLogRuleTestsAsync()
        {
            CreateApplicationLogRule rule = new CreateApplicationLogRule(
                SystemManager.ServiceProvider.GetRequiredService<ILoggerFactory>(),
                SystemManager.ServiceProvider.GetRequiredService<ILogMessageApiService>(),
                SystemManager.ServiceProvider.GetRequiredService<IMapper>());

            BusinessRuleContext context = new BusinessRuleContext();
            context.Object = new CreateApplicationLogBroadcast(new ApplicationLogDto()
            {
            });

            await rule.ExecuteRuleAsync(context);
        }
    }
}