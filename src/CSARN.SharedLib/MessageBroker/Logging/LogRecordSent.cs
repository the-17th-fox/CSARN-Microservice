using Microsoft.Extensions.Logging;

namespace CSARN.SharedLib.MessageBroker
{
    public record LogRecordSent
    {
        public string ProducerService { get; init; } = string.Empty;
        public LogLevel Level { get; init; }
        public string Message { get; init; } = string.Empty;
        public object[]? Args { get; init; }
    }
}
