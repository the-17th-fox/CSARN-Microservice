using SharedLib.AccountsMsvc.Models;

namespace Core.Interfaces.Repositories
{
    public interface ITokensRepository
    {
        public Task RemoveRefreshTokenAsync(Guid accountId);
        public Task RevokeRefreshTokenAsync(Guid accountId);
        public Task RevokeAllRefreshTokensAsync();
        public Task<RefreshToken> IssueRefreshTokenAsync(RefreshToken newRefreshToken);
    }
}
