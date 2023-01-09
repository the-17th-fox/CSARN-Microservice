using Core.Interfaces.Services;
using Core.ViewModels.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Auth;

namespace Web.Controllers.Accounts
{
    [Authorize]
    [Route("api/tokens")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokensService _tokensSvc;

        public TokensController(ITokensService tokensSvc)
        {
            _tokensSvc = tokensSvc;
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessTokenAsync([FromBody] TokensRefreshingViewModel tokenViewModel)
        {
            var tokensPair = await _tokensSvc.RefreshAccessTokenAsync(tokenViewModel.RefreshToken.ToString(), tokenViewModel.AccessToken);
            return Ok(tokensPair);
        }

        [Authorize(Policy = AccountsPolicies.Administrators)]
        [HttpPatch("revoke/{accountId}")]
        public async Task<IActionResult> RevokeRefreshTokenAsync(Guid accountId)
        {
            await _tokensSvc.RevokeRefreshTokenAsync(accountId);
            return Ok();
        }

        [Authorize(Policy = AccountsPolicies.Administrators)]
        [HttpPatch("revoke/all")]
        public async Task<IActionResult> RevokeAllRefreshTokensAsync()
        {
            await _tokensSvc.RevokeAllRefreshTokensAsync();
            return Ok();
        }
    }
}
