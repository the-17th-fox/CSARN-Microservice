using Core.DomainServicesAbstractions;
using Core.Models;
using Core.ViewModels;
using CSARN.SharedLib.Utilities;
using CSARN.SharedLib.ViewModels.Pagination;

namespace Core.ApplicationServices
{
    public class LogsSerivce : ILogsService
    {
        private readonly ILoggingRepository _repository = null!;

        public LogsSerivce(ILoggingRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<LoggingRecord>> GetAllAsync(
            PageParametersViewModel pageParams, SearchParametersViewModel searchParams)
        {
            var pagedLogs = await _repository.GetAllAsync(searchParams, pageParams);
            return pagedLogs;
        }
    }
}
