using Microsoft.Extensions.Logging;

namespace Core.Utilities
{
    public class LogsHandler<T>
    {
        private readonly ILogger<T> _logger;

        public LogsHandler(ILogger<T> logger) => _logger = logger;

        public void Log(LogLevel logLevel, string message, params string[] args)
        {
            _logger.Log(logLevel, message, args);
        }
    }
}
