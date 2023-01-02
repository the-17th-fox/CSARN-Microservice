using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.ViewModels.Passports;
using CSARN.SharedLib.Constants.CustomExceptions;
using SharedLib.AccountsMsvc.Misc;
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

        private async Task<Passport> CheckIfExistsAsync(Guid accountId)
        {
            var passport = await _passportsRep.GetByAccountIdAsync(accountId);
            if (passport == null)
                throw new NotFoundException("Passport with specified accountId wasn't found.");

            return passport;
        }

        public async Task<Passport> GetByAccountIdAsync(Guid accountId)
        {
            var passport = await CheckIfExistsAsync(accountId);

            return passport;
        }

        public async Task<Passport> GetByPersonalInfoAsync(string firstName, string lastName, string patronymic)
        {
            var passport = await _passportsRep.GetByPersonalInfo(firstName, lastName, patronymic);
            if (passport == null)
                throw new NotFoundException("Passport with specified information wasn't found.");

            return passport;
        }

        public async Task UpdateAsync(Guid accountId, UpdatePassportModel passportModel)
        {
            var passport = await CheckIfExistsAsync(accountId);

            if (passportModel.RegionCode == RegionCodes.Undefined)
                throw new BadRequestException("Region code is undefined.");

            await _passportsRep.UpdateAsync(accountId, passportModel);
        }
    }
}
