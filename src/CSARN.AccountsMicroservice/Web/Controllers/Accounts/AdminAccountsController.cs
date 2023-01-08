using AutoMapper;
using Core.Interfaces.Services;
using Core.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Auth;
using System.Security.Claims;

namespace Web.Controllers.Accounts
{
    [Authorize(Policy = AccountsPolicies.Administrators)]
    [Route("api/accounts/admin")]
    [ApiController]
    public class AdminAccountsController : ControllerBase
    {
        private readonly IAccountsService _accSvc;
        private IMapper _mapper;

        private Guid _accountId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public AdminAccountsController(IAccountsService accSvc, IMapper mapper)
        {
            _accSvc = accSvc;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, bool returnDeleted, bool returnBlocked)
        {
            var acc = await _accSvc.GetByIdAsync(id, returnDeleted, returnBlocked);
            var roles = await _accSvc.GetRolesAsync(id);

            var model = _mapper.Map<ExtendedAccountViewModel>(acc);
            model.Roles = roles;

            return Ok(model);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAllAsync([FromBody] AccPaginationViewModel paginationViewModel)
        {
            var accs = await _accSvc.GetAllAsync(paginationViewModel);

            var modelsList = _mapper.Map<List<ExtendedAccountViewModel>>(accs);
            modelsList.ForEach(async m => m.Roles = await _accSvc.GetRolesAsync(m.Id));

            return Ok(modelsList);
        }

        [HttpPatch("block/{id}")]
        public async Task<IActionResult> BlockAsync(Guid id)
        {
            await _accSvc.BlockAsync(id);
            return Ok();
        }

        [HttpPatch("unblock/{id}")]
        public async Task<IActionResult> UnblockAsync(Guid id)
        {
            await _accSvc.UnblockAsync(id);
            return Ok();
        }

        [HttpPatch("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _accSvc.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("clear-access-failed-counter/{id}")]
        public async Task<IActionResult> ClearAccessFailedCounterAsync(Guid id)
        {
            await _accSvc.ClearAccessFailedCounterAsync(id);
            return Ok();
        }

        [HttpPatch("change-role/{id}")]
        public async Task<IActionResult> ChangeRoleAsync(Guid id, string newRoleName)
        {
            await _accSvc.ChangeRoleAsync(id, newRoleName);
            return Ok();
        }
    }
}
