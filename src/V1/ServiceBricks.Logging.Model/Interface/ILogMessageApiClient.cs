namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a REST API client for the LogMessageDto.
    /// </summary>
    public partial interface ILogMessageApiClient : IApiClient<LogMessageDto>, ILogMessageApiService
    {
    }
}