using AutoMapper;
using Core.Interfaces.Services;
using Core.ViewModels.Passports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Auth;
using System.Security.Claims;

namespace Web.Controllers.Passports
{
    [Authorize(Roles = AccountsRoles.MIA)]
    [Route("api/passports/mia")]
    [ApiController]
    public class MIAPassportsController : ControllerBase
    {
        private readonly IPassportsService _passSvc;
        private IMapper _mapper;

        private Guid _accountId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        public MIAPassportsController(IPassportsService passSvc, IMapper mapper)
        {
            _passSvc = passSvc ?? throw new ArgumentNullException(nameof(passSvc));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));   
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetByPersonalInfoAsync([FromBody] SearchPassportViewModel searchPassport)
        {
            var pass = await _passSvc.GetByPersonalInfoAsync(searchPassport.FirstName, searchPassport.LastName, searchPassport.Patronymic);
            var model = _mapper.Map<PassportViewModel>(pass);

            return Ok(model);
        }
    }
}
