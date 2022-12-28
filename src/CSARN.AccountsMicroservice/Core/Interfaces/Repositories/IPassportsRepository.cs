using Core.Domain.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;

namespace Core.Interfaces.Repositories
{
    public interface IPassportsRepository
    {
        public Task<Passport?> GetByAccountIdAsync(Guid accountId);
        public Task<Passport?> GetByPersonalInfo(string firstName, string lastName, string patronymic);
        public Task UpdateAsync(Guid accountId, UpdatePassportModel passportModel);
    }
}
