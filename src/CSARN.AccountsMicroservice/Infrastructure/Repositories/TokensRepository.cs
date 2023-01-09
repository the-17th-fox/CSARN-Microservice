using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using SharedLib.AccountsMsvc.Models;

namespace Infrastructure.Repositories
{
    public class TokensRepository : ITokensRepository
    {
        private readonly AccountsContext _context;

        public TokensRepository(AccountsContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> IssueRefreshTokenAsync(RefreshToken newRefreshToken)
        {
            var refrToken = await _context.RefreshTokens.Where(rt => rt.AccountId == newRefreshToken.AccountId).FirstOrDefaultAsync();

            if(refrToken == null)
            {
                _context.RefreshTokens.Add(newRefreshToken);
                await _context.SaveChangesAsync();
                return newRefreshToken;
            }
            
            _context.RefreshTokens.Update(refrToken);
            refrToken.ExpiresAt = newRefreshToken.ExpiresAt;
            refrToken.IssuedAt = newRefreshToken.IssuedAt;
            refrToken.AccountId = newRefreshToken.AccountId;
            refrToken.IsRevoked = false;
            await _context.SaveChangesAsync();
            return refrToken;
        }

        public async Task RemoveRefreshTokenAsync(Guid accountId)
        {
            var refrToken = await _context.RefreshTokens.Where(rt => rt.AccountId == accountId).FirstOrDefaultAsync();
            if (refrToken == null)
                throw new KeyNotFoundException(nameof(refrToken));

            _context.RefreshTokens.Remove(refrToken);
            await _context.SaveChangesAsync();
        }

        public Task RevokeAllRefreshTokensAsync()
        {
            throw new NotImplementedException();
        }

        public Task RevokeRefreshTokenAsync(Guid accountId)
        {
            throw new NotImplementedException();
        }
    }
}
