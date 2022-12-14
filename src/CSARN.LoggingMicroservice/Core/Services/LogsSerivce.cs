using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModels;
using CSARN.SharedLib.Utilities;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class LogsSerivce : ILogsService
    {
        private readonly ILoggingRepository _repository = null!;
        private readonly ILogger<LogsSerivce> _logger;

        public LogsSerivce(ILoggingRepository repository, ILogger<LogsSerivce> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task AddAsync(LogLevel logLevel, string message, params object?[] args)
        {
            await new Task(() => _logger.Log(logLevel, message, args));
        }

        public async Task<PagedList<LoggingRecord>> GetAllAsync(SearchParametersViewModel searchParams)
        {
            var pagedLogs = await _repository.GetAllAsync(searchParams);
            return pagedLogs;
        }

        
    }
}
