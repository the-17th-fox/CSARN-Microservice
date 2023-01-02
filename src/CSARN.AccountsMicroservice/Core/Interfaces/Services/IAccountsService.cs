using Core.Domain.ViewModels.Accounts;
using Core.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;

namespace Core.Interfaces.Services
{
    public interface IAccountsService
    {
        public Task CreateAsync(RegistrationViewModel regParams);
        public Task<Account> GetByIdAsync(Guid id, bool returnDeleted, bool returnBlocked = true);
        public Task<List<Account>> GetAllAsync(AccPaginationViewModel pageParams);
        public Task BlockAsync(Guid id);
        public Task UnblockAsync(Guid id);
        public Task DeleteAsync(Guid id);
        public Task<string> LoginAsync(LoginViewModel loginViewModel);
        public Task ChangeRoleAsync(Guid id, string roleName);
        public Task<IList<string>> GetRolesAsync(Guid accountId);
        // TODO: Add acc restoring method
    }
}
