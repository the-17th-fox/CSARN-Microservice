using AutoMapper;
using Core.Interfaces.Services;
using Core.ViewModels.Passports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Auth;
using System.Security.Claims;

namespace Web.Controllers.Passports
{
    /// <summary>
    /// TODO: Add logger
    /// </summary>

    [Authorize(Policy = AccountsPolicies.DefaultRights)]
    [Route("api/passports")]
    [ApiController]
    public class PublicPassportsController : ControllerBase
    {
        private readonly IPassportsService _passSvc;
        private IMapper _mapper;

        private Guid _accountId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public PublicPassportsController(IPassportsService passSvc, IMapper mapper)
        {
            _passSvc = passSvc ?? throw new ArgumentNullException(nameof(passSvc));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyPassportAsync()
        {
            var pass = await _passSvc.GetByAccountIdAsync(_accountId);
            var model = _mapper.Map<PassportViewModel>(pass);

            return Ok(model);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdatePassportModel passportModel)
        {
            await _passSvc.UpdateAsync(_accountId, passportModel);

            return Ok();
        }
    }
}
