using Microsoft.Extensions.Logging;

namespace Core.ViewModels
{
    public class NewLogViewModel
    {
        public string ProducerService { get; set; } = string.Empty;
        public LogLevel Level { get; set; }
        public string Message { get; set; } = string.Empty;
        public object[]? Args { get; set; }
    }
}
