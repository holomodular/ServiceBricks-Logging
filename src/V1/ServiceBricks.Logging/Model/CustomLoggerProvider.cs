using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ServiceBricks.Logging
{
    /// <summary>
    /// This is a custom logger provider that creates instances of the CustomLogger.
    /// </summary>
    [ProviderAlias("CustomLogger")]
    public sealed class CustomLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private readonly ApplicationOptions _applicationOptions;
        private IExternalScopeProvider _externalScopeProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appOptions"></param>
        public CustomLoggerProvider(
            IOptions<ApplicationOptions> appOptions)
        {
            _applicationOptions = appOptions.Value;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Create logger
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(
                categoryName,
                _applicationOptions.Name,
                _externalScopeProvider);
        }

        /// <summary>
        /// Set scope provider
        /// </summary>
        /// <param name="scopeProvider"></param>
        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _externalScopeProvider = scopeProvider;
        }
    }
}