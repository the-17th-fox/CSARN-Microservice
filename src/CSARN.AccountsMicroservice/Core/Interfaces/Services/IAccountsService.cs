using Core.Domain.ViewModels.Accounts;
using Core.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;

namespace Core.Interfaces.Services
{
    public interface IAccountsService
    {
        // Auth methods
        public Task CreateAsync(RegistrationViewModel regParams);
        public Task<TokenViewModel> LoginAsync(LoginViewModel loginViewModel);

        // Accs management methods 
        public Task BlockAsync(Guid id);
        public Task UnblockAsync(Guid id);
        public Task ClearAccessFailedCounterAsync(Guid id);
        public Task DeleteAsync(Guid id);
        public Task<Account> GetByIdAsync(Guid id, bool returnDeleted, bool returnBlocked = true);
        public Task<List<Account>> GetAllAsync(AccPaginationViewModel pageParams);
        public Task<IList<string>> GetRolesAsync(Guid id);
        public Task ChangeRoleAsync(Guid id, string roleName);
        
        // TODO: Add acc restoring method
    }
}
