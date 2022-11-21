using Core.Models;
using Core.ViewModels;
using CSARN.SharedLib.Utilities;
using CSARN.SharedLib.ViewModels.Pagination;

namespace Core.DomainServicesAbstractions
{
    public interface ILoggingRepository
    {
        public Task<PagedList<LoggingRecord>> GetAllAsync(SearchParametersViewModel searchParams, PageParametersViewModel pageParams);
    }
}
