using Core.Models;
using Core.ViewModels;
using CSARN.SharedLib.Utilities;

namespace Core.Interfaces.Repositories
{
    public interface ILoggingRepository
    {
        public Task<PagedList<LoggingRecord>> GetAllAsync(SearchParametersViewModel searchParams);
    }
}
