using Core.Domain.ViewModels.Accounts;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using SharedLib.AccountsMsvc.Misc;
using SharedLib.AccountsMsvc.Models;

namespace Infrastructure.Repositories
{
    public class PassportsRepository : IPassportsRepository
    {
        private readonly AccountsContext _dbContext;

        public PassportsRepository(AccountsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Passport?> GetByAccountIdAsync(Guid accountId)
        {
            return await _dbContext.Passports.FirstOrDefaultAsync(p => p.AccountId == accountId);
        }

        public async Task<Passport?> GetByPersonalInfo(string firstName, string lastName, string patronymic)
        {
            return await _dbContext.Passports
                .AsNoTracking()
                .Where(p => p.FirstName == firstName)
                .Where(p => p.LastName == lastName)
                .Where(p => p.Patronymic == patronymic)
                .FirstOrDefaultAsync()!;
        }

        public async Task UpdateAsync(Guid accountId, UpdatePassportModel passportModel)
        {
            var passport = await _dbContext.Passports.FirstOrDefaultAsync(p => p.AccountId == accountId);

            _dbContext.Passports.Update(passport!);

            passport!.FirstName = passportModel.FirstName;
            passport!.LastName = passportModel.LastName;
            passport!.Patronymic = passportModel.Patronymic;
            passport!.Region = Enum.GetName<RegionCodes>(passportModel.RegionCode) ?? throw new Exception("Region name parsing has failed.");

            await _dbContext.SaveChangesAsync();
        }
    }
}
