using Core.Models;
using Core.ViewModels;
using CSARN.SharedLib.Utilities;
using Microsoft.Extensions.Logging;

namespace Core.Interfaces.Services
{
    public interface ILogsService
    {
        public Task<PagedList<LoggingRecord>> GetAllAsync(SearchParametersViewModel searchParams);
        public Task AddAsync(NewLogViewModel viewModel);
    }
}
