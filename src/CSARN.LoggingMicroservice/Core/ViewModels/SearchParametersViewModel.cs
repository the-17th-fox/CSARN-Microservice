using CSARN.SharedLib.ViewModels;
using CSARN.SharedLib.ViewModels.Pagination;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels
{
    public class SearchParametersViewModel : PageParametersViewModel
    {
        [Range(minimum: 1, maximum: 31, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public byte FromDay { get; set; } = 1;

        [Range(minimum: 1, maximum: 12, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public byte FromMonth { get; set; } = (byte)DateTime.UtcNow.Month;

        [Range(minimum: 2022, maximum: short.MaxValue, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public short FromYear { get; set; } = (short)DateTime.UtcNow.Year;

        [Range(minimum: 0, maximum: 6, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public LogLevel LowestLoggingLevel { get; set; } = LogLevel.Debug;
    }
}
