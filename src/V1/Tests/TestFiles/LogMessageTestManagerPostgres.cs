using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBricks.Logging;
using ServiceQuery;

namespace ServiceBricks.Xunit
{
    public class LogMessageTestManagerPostgres : LogMessageTestManager
    {
        public override void ValidateObjects(LogMessageDto clientDto, LogMessageDto serviceDto, HttpMethod method)
        {
            //CreateDateRule
            if (method == HttpMethod.Post)
                Assert.True(serviceDto.CreateDate >= clientDto.CreateDate); //Rule
            else if (method == HttpMethod.Get)
            {
                // Postgres special handling
                long utcTicks = serviceDto.CreateDate.UtcTicks;
                utcTicks = ((long)(utcTicks / 10)) * 10;
                Assert.True(utcTicks == clientDto.CreateDate.UtcTicks);
            }
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
    }
}