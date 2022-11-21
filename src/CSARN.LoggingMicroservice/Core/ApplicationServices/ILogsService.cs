using Core.Models;
using Core.ViewModels;
using CSARN.SharedLib.Utilities;
using CSARN.SharedLib.ViewModels.Pagination;
using Microsoft.Extensions.Logging;

namespace Core.ApplicationServices
{
    public interface ILogsService
    {
        public Task<PagedList<LoggingRecord>> GetAllAsync(PageParametersViewModel pageParams, SearchParametersViewModel searchParams);
    }
}
