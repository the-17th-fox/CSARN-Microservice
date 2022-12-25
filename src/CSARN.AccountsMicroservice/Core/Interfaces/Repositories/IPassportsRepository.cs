using Core.Domain.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;

namespace Core.Interfaces.Repositories
{
    public interface IPassportsRepository
    {
        public Task<Passport?> GetByIdAsync(Guid id);
        public Task<Passport?> GetByPersonalInfo(string firstName, string lastName, string patronymic);
        public Task UpdateAsync(Guid id, UpdatePassportModel passportModel);
    }
}
