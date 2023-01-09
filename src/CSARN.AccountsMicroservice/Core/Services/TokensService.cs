using Core.Interfaces.Repositories;
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
        private readonly ITokensRepository _tokensRep;

        public TokensService(IOptions<JwtConfigModel> jwtConfig, UserManager<Account> userManager, ITokensRepository tokensRepository)
        {
            _jwtConfig = jwtConfig.Value;
            _userManager = userManager;
            _tokensRep = tokensRepository;
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

        private RefreshToken GenerateRefreshToken(Guid accountId)
        {
            var expAt = _jwtConfig.RefreshTokenLifetimeInDays;
            return new RefreshToken(DateTime.UtcNow.AddDays(expAt), accountId);
        }

        public async Task<RefreshToken> IssueRefreshTokenAsync(Guid accountId)
        {
            var newRefrToken = GenerateRefreshToken(accountId);
            return await _tokensRep.IssueRefreshTokenAsync(newRefrToken);
        }

        public async Task<TokensViewModel> RefreshAccessTokenAsync(string refreshToken, string accessToken)
        {
            var principal = GetClaimsPrincipalFromToken(accessToken);
            if(principal == null)
                throw new BadRequestException("Invalid access token or refresh token.");

            string? username = principal?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new BadRequestException("Could not get a username from the claims.");

            var acc = await _userManager.Users
                .Where(a => a.UserName == username)
                .Include(a => a.RefreshToken)
                .FirstOrDefaultAsync();
            if (acc == null)
                throw new NotFoundException("There is no account with the specified username.");

            if (acc.RefreshToken == null || !acc.RefreshToken.IsActive || !acc.RefreshToken.Token.ToString().Equals(refreshToken))
                throw new UnauthorizedException("Account's refresh token is null, expired, revoked or doesn't equal to provided refresh token.");

            var newRefrToken = await IssueRefreshTokenAsync(acc.Id);

            var newAccessToken = CreateAccessToken(principal!.Claims.ToList());       

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefrToken.Token,
                RefreshTokenExpiresAt = newRefrToken.ExpiresAt,
                AccessTokenExpiresAt = newAccessToken.ValidTo
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

        public async Task RevokeAllRefreshTokensAsync() => await _tokensRep.RevokeAllRefreshTokensAsync();

        public async Task RevokeRefreshTokenAsync(Guid accountId) => await _tokensRep.RevokeRefreshTokenAsync(accountId);
    }
}
