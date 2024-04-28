using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a provider for the custom database logger.
    /// </summary>
    [ProviderAlias("CustomLogger")]
    public sealed class CustomLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private readonly ApplicationOptions _applicationOptions;
        private IExternalScopeProvider _externalScopeProvider;

        public CustomLoggerProvider(
            IOptions<ApplicationOptions> appOptions)
        {
            _applicationOptions = appOptions.Value;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(
                categoryName,
                _applicationOptions.Name,
                _externalScopeProvider);
        }

        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _externalScopeProvider = scopeProvider;
        }
    }
}