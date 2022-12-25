using Core.Domain.ViewModels.Accounts;
using Core.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;

namespace Core.Interfaces.Services
{
    public interface IAccountsService
    {
        public Task CreateAsync(RegistrationParametersModel regParams);
        public Task<Account> GetByIdAsync(Guid id);
        public Task<List<Account>> GetAllAsync(AccPaginationViewModel pageParams);
        public Task BlockAsync(Guid id);
        public Task UnblockAsyc(Guid id);
        public Task DeleteAsync(Guid id);
        public Task<string> LoginAsync(LoginViewModel loginViewModel);
        public Task ChangeRoleAsync(Guid id, string roleName);
        // TODO: Add acc restoring method
    }
}
