using Core.Domain.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;

namespace Core.Interfaces.Services
{
    public interface IPassportsService
    {
        public Task<Passport> GetByAccountIdAsync(Guid accountId);
        public Task<Passport> GetByPersonalInfoAsync(string firstName, string lastName, string patronymic);
        public Task UpdateAsync(Guid accountId, UpdatePassportModel passportModel);
    }
}
