using AutoMapper;
using Core.Constants;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModels;
using CSARN.SharedLib.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controller
{
    [Route("api/logs")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogsService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<LogsController> _logger;

        private string _userId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        public LogsController(ILogsService service,
                              IMapper mapper,
                              ILogger<LogsController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetAllAsync([FromBody] SearchParametersViewModel searchParams)
        {
            _logger.LogInformation(LogEvents.LogsRetrievingAttempt, _userId);

            var records = await _service.GetAllAsync(searchParams);
            var recordsPage = _mapper.Map<PageViewModel<LoggingRecord>>(records);

            _logger.LogInformation(LogEvents.LogsRetrieved, _userId);
            return Ok(recordsPage);
        }
    }
}
