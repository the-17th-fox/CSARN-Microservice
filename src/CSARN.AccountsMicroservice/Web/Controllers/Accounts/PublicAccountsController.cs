using AutoMapper;
using Core.Domain.ViewModels.Accounts;
using Core.Interfaces.Services;
using Core.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Auth;
using System.Security.Claims;

namespace Web.Controllers.Accounts
{
    /// <summary>
    /// TODO: Add logger
    /// </summary>

    [Authorize]
    [Route("api/accounts")]
    [ApiController]
    public class PublicAccountsController : ControllerBase
    {
        private readonly IAccountsService _accSvc;
        private IMapper _mapper;

        private Guid _accountId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public PublicAccountsController(IAccountsService accSvc, IMapper mapper)
        {
            _accSvc = accSvc;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationViewModel registrationModel)
        {
            await _accSvc.CreateAsync(registrationModel);
            return Created("register", null);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel loginModel)
        {
            var tokensPair = await _accSvc.LoginAsync(loginModel);
            return Ok(tokensPair);
        }

        [Authorize(Policy = AccountsPolicies.DefaultRights)]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyProfileAsync()
        {
            var account = await _accSvc.GetByIdAsync(_accountId, returnDeleted: false);
            var model = _mapper.Map<AccountViewModel>(account);
            model.Roles = await _accSvc.GetRolesAsync(_accountId);
            return Ok(model);
        }

        [Authorize(Policy = AccountsPolicies.DefaultRights)]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync()
        {
            await _accSvc.DeleteAsync(_accountId);
            return Ok();
        }
    }
}
