using Core.Interfaces.Services;
using Core.Utilities;
using Core.ViewModels.Accounts;
using CSARN.SharedLib.Constants.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLib.AccountsMsvc.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Services
{
    public class TokensService : ITokensService
    {
        private readonly JwtConfigModel _jwtConfig;
        private readonly UserManager<Account> _userManager;

        public TokensService(IOptions<JwtConfigModel> jwtConfig, UserManager<Account> userManager)
        {
            _jwtConfig = jwtConfig.Value;
            _userManager = userManager;
        }

        public JwtSecurityToken CreateAccessToken(IList<Claim> claims)
        {
            var symSecurityKey = _jwtConfig.Key;
            return new(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.AuthTokenLifetimeInMinutes),
                signingCredentials: new SigningCredentials(symSecurityKey, SecurityAlgorithms.HmacSha256));
        }

        public RefreshToken GenerateRefreshToken()
        {
            var expAt = _jwtConfig.RefreshTokenLifetimeInDays;
            return new RefreshToken(DateTime.UtcNow.AddDays(expAt));
        }

        public async Task<TokenViewModel> RefreshAccessTokenAsync(string refreshToken, string accessToken)
        {
            var principal = GetClaimsPrincipalFromToken(accessToken);
            if(principal == null)
                throw new BadRequestException("Invalid access token or refresh token.");

            string? username = principal?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new BadRequestException("Could not get a username from the claims.");

            var acc = await _userManager.FindByNameAsync(username);
            if (acc == null)
                throw new NotFoundException("There is no account with the specified username.");

            if (acc.RefreshToken == null || !acc.RefreshToken.IsActive || !acc.RefreshToken.Token.ToString().Equals(refreshToken))
                throw new UnauthorizedException("Account's refresh token is null, expired, revoked or doesn't equal to provided refresh token.");

            var newRefrToken = GenerateRefreshToken();
            acc.RefreshToken = newRefrToken;
            await _userManager.UpdateAsync(acc);

            var newAccessToken = CreateAccessToken(principal!.Claims.ToList());       

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefrToken.Token
            };
        }

        private ClaimsPrincipal? GetClaimsPrincipalFromToken(string accessToken)
        {
            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _jwtConfig.Key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParams, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken 
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid access token or refresh token.");

            return principal;
        }

        public async Task RevokeAllRefreshTokensAsync()
        {
            foreach (var account in await _userManager.Users.Include(a => a.RefreshToken).ToListAsync())
            {
                if (account.RefreshToken != null)
                {
                    account.RefreshToken.IsRevoked = true;
                    await _userManager.UpdateAsync(account);
                }
            }
        }

        public async Task RevokeRefreshTokenAsync(Guid accountId)
        {
            var acc = await _userManager.FindByIdAsync(accountId.ToString());

            if (acc == null)
                throw new NotFoundException("There is no account with the specified id.");            

            if (acc.RefreshToken == null)
                throw new BadRequestException("Account doesn't have a refresh token.");

            if (!acc.RefreshToken.IsActive)
                throw new BadRequestException("Refresh token has already expired or been revoked.");

            acc.RefreshToken.IsRevoked = true;
            await _userManager.UpdateAsync(acc);
        }
    }
}
