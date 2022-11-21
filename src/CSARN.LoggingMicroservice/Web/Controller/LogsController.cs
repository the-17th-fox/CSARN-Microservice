using AutoMapper;
using Core.ApplicationServices;
using Core.Constants;
using Core.Models;
using Core.Utilities;
using Core.ViewModels;
using CSARN.SharedLib.ViewModels.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using System.Security.Claims;

namespace Web.Controller
{
    [Route("api/logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogsService _service;
        private readonly IMapper _mapper;
        private readonly LogsHandler<LogsController> _logsHandler;

        private string _userId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public LogsController(ILogsService service, IMapper mapper, ILogger<LogsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logsHandler = new(logger);
        }

        [HttpGet("{PageNumber}-{PageSize}/{FromDay}-{FromMonth}-{FromYear}/{LowestLoggingLevel}")]
        public async Task<IActionResult> GetAllAsync(PageParametersViewModel pageParams, SearchParametersViewModel searchParams)
        {
            _logsHandler.Log(LogLevel.Information, LogEvents.LogsRetrievingAttempt, _userId);

            var records = await _service.GetAllAsync(pageParams, searchParams);
            var recordsPage = _mapper.Map<PageViewModel<LoggingRecord>>(records);

            _logsHandler.Log(LogLevel.Information, LogEvents.LogsRetrieved, _userId);
            return Ok(recordsPage);
        }
    }
}
