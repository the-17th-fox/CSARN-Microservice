using Core.Domain.ViewModels.Accounts;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using CSARN.SharedLib.Constants.CustomExceptions;
using SharedLib.AccountsMsvc.Models;

namespace Core.Services
{
    public class PassportsService : IPassportsService
    {
        private readonly IPassportsRepository _passportsRep;

        public PassportsService(IPassportsRepository passportsRep)
        {
            _passportsRep = passportsRep;
        }

        public async Task<Passport> GetByIdAsync(Guid id)
        {
            var passport = await _passportsRep.GetByIdAsync(id);
            if (passport == null)
                throw new NotFoundException("Passport with specified id wasn't found.");

            return passport;
        }

        public async Task<Passport> GetByPersonalInfo(string firstName, string lastName, string patronymic)
        {
            var passport = await _passportsRep.GetByPersonalInfo(firstName, lastName, patronymic);
            if (passport == null)
                throw new NotFoundException("Passport with specified information wasn't found.");

            return passport;
        }

        public async Task UpdateAsync(Guid id, UpdatePassportModel passportModel)
        {
            var passport = await _passportsRep.GetByIdAsync(id);
            if (passport == null)
                throw new NotFoundException("Passport with specified id wasn't found.");

             await _passportsRep.UpdateAsync(id, passportModel);
        }
    }
}
