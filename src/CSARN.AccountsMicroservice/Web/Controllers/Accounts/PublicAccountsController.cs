using AutoMapper;
using Core.Constants;
using Core.Domain.ViewModels.Accounts;
using Core.Interfaces.Services;
using Core.ViewModels.Accounts;
using CSARN.SharedLib.MessageBroker.Logging;
using MassTransit;
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
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _pubEndp;

        private Guid _accountId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public PublicAccountsController(IAccountsService accSvc, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _accSvc = accSvc;
            _mapper = mapper;
            _pubEndp = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
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
            await _pubEndp.LogInformationAsync("accounts-msvc", LogEvents.LoginAttempt, loginModel.Email);

            var tokensPair = await _accSvc.LoginAsync(loginModel);

            await _pubEndp.LogInformationAsync("accounts-msvc", LogEvents.TokenReturned, loginModel.Email);
            
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
