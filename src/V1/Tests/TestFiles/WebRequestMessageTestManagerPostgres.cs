using ServiceBricks.Logging;

namespace ServiceBricks.Xunit
{
    public class WebRequestMessageTestManagerPostgres : WebRequestMessageTestManager
    {
        public override void ValidateObjects(WebRequestMessageDto clientDto, WebRequestMessageDto serviceDto, HttpMethod method)
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
    }
}