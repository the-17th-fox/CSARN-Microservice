using Core.Domain.ViewModels.Accounts;
using SharedLib.AccountsMsvc.Models;

namespace Core.Interfaces.Services
{
    public interface IPassportsService
    {
        public Task<Passport> GetByIdAsync(Guid id);
        public Task<Passport> GetByPersonalInfo(string firstName, string lastName, string patronymic);
        public Task UpdateAsync(Guid id, UpdatePassportModel passportModel);
    }
}
