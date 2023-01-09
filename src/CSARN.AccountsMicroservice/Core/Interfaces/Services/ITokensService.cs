using Core.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Interfaces.Services
{
    public interface ITokensService
    {
        public Task<TokensViewModel> RefreshAccessTokenAsync(string refreshToken, string accessToken);
        public Task<RefreshToken> IssueRefreshTokenAsync(Guid accountId);
        public Task RevokeRefreshTokenAsync(Guid accountId);
        public Task RevokeAllRefreshTokensAsync();
        public JwtSecurityToken CreateAccessToken(IList<Claim> claims);
    }
}
