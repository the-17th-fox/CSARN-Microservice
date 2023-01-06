using Core.Domain.ViewModels.Accounts;
using Core.Interfaces.Services;
using Core.Utilities;
using Core.ViewModels.Accounts;
using CSARN.SharedLib.Constants.CustomExceptions;
using CSARN.SharedLib.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLib.AccountsMsvc.Misc;
using SharedLib.AccountsMsvc.Models;
using SharedLib.Auth;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Transactions;

namespace Core.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<Account> _userManager;
        private readonly ITokensService _tokensSvc;

        public AccountsService(UserManager<Account> userManager, ITokensService tokensSvc)
        {
            _userManager = userManager;
            _tokensSvc = tokensSvc;
        }
        
        /// Auth methods below

        public async Task CreateAsync(RegistrationViewModel regParams)
        {
            if (regParams.PassRegionCode == SharedLib.AccountsMsvc.Misc.RegionCodes.Undefined)
                throw new InvalidParamsException("Region code is undefiend.");

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var acc = new Account()
                {
                    UserName = regParams.Email,
                    PhoneNumber = regParams.PhoneNumber,
                    Email = regParams.Email
                };

                var pass = new Passport()
                {
                    Account = acc,
                    FirstName = regParams.FirstName.ToUpperInvariant(),
                    LastName = regParams.LastName.ToUpperInvariant(),
                    Patronymic = regParams.Patronymic.ToUpperInvariant(),
                    Region = Enum.GetName<RegionCodes>(regParams.PassRegionCode) ?? throw new Exception("Can not get the name of the region"),
                    Number = regParams.PassNumber,
                };

                acc.Passport = pass;

                var result = await _userManager.CreateAsync(acc, regParams.Password);
                if (!result.Succeeded)
                    throw new Exception("User creation failed: " + result.Errors.First<IdentityError>().Description);

                result = await _userManager.AddToRoleAsync(acc, AccountsRoles.Citizen);
                if (!result.Succeeded)
                    throw new Exception("Adding to role failed: " + result.Errors.First<IdentityError>().Description);

                scope.Complete();
            }
            catch (Exception)
            {
                scope.Dispose();
                throw;
            }
        }

        public async Task<TokenViewModel> LoginAsync(LoginViewModel loginViewModel)
        {
            var acc = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (acc == null)
                throw new NotFoundException($"There is no account with the specified email.");

            if (acc.IsDeleted || acc.IsBlocked)
                throw new BadRequestException("Account is deleted or blocked.");

            var isCorrect = await _userManager.CheckPasswordAsync(acc, loginViewModel.Password);
            if (!isCorrect)
            {
                acc.AccessFailedCount++;
                if (acc.AccessFailedCount == 5)
                {
                    acc.IsBlocked = true;

                    if (!(await _userManager.UpdateAsync(acc)).Succeeded)
                        throw new Exception("Updating account's blocked status has failed.");
                }
                throw new UnauthorizedException("Password check has failed.");
            }

            acc.AccessFailedCount = 0;

            var accRoles = await _userManager.GetRolesAsync(acc);
            var userClaims = GetClaims(acc, accRoles);

            var accessToken = _tokensSvc.CreateAccessToken(userClaims);

            if (acc.RefreshTokenId != null)
                await _tokensSvc.RevokeRefreshTokenAsync(acc.Id);

            var refrToken = _tokensSvc.GenerateRefreshToken();
            acc.RefreshToken = refrToken;
            await _userManager.UpdateAsync(acc);

            return new TokenViewModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refrToken.Token
            };
        }

        private static List<Claim> GetClaims(Account account, IList<string> accountRoles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.UserName),
            };

            foreach (var role in accountRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public async Task<IList<string>> GetRolesAsync(Guid id)
        {
            var acc = await CheckIfExistsAsync(id);
            return await _userManager.GetRolesAsync(acc);
        }

        /// Accounts management methods below

        public async Task BlockAsync(Guid id)
        {
            var acc = await CheckIfExistsAsync(id);

            if (acc.IsBlocked)
                throw new BadRequestException("Account has been already blocked.");

            acc.IsBlocked = true;

            if (!(await _userManager.UpdateAsync(acc)).Succeeded)
                throw new Exception("Updating account's blocked status has failed.");
        }

        public async Task UnblockAsync(Guid id)
        {
            var acc = await CheckIfExistsAsync(id);

            if (!acc.IsBlocked)
                throw new BadRequestException("Account isn't blocked.");

            acc.IsBlocked = false;

            if (!(await _userManager.UpdateAsync(acc)).Succeeded)
                throw new Exception("Updating account's blocked status has failed.");
        }

        public async Task ClearAccessFailedCounterAsync(Guid id)
        {
            var acc = await CheckIfExistsAsync(id);

            acc.AccessFailedCount = 0;

            if (!(await _userManager.UpdateAsync(acc)).Succeeded)
                throw new Exception("Clearing account's access failed counter has failed.");
        }

        public async Task DeleteAsync(Guid id)
        {
            var acc = await CheckIfExistsAsync(id);

            if (acc.IsDeleted)
                throw new BadRequestException("Account has been already deleted.");

            acc.IsDeleted = true;

            if (!(await _userManager.UpdateAsync(acc)).Succeeded)
                throw new Exception("Account deleting has failed.");
        }

        public async Task<List<Account>> GetAllAsync(AccPaginationViewModel pageParams)
        {
            var query = _userManager.Users;

            if (!pageParams.ShowDeleted)
                query.Where(u => u.IsDeleted == false);

            if (!pageParams.ShowBlocked)
                query.Where(u => u.IsBlocked == false);

            return await PagedList<Account>.ToPagedListAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public async Task<Account> GetByIdAsync(Guid id, bool returnDeleted, bool returnBlocked = true)
        {
            var acc = await CheckIfExistsAsync(id);

            if (acc.IsDeleted && !returnDeleted)
                throw new BadRequestException("Requested account is deleted.");

            if (acc.IsBlocked && !returnBlocked)
                throw new BadRequestException("Requested account is blocked.");

            return acc;
        }

        public async Task ChangeRoleAsync(Guid id, string roleName)
        {
            var acc = await CheckIfExistsAsync(id);

            if (acc.IsDeleted || acc.IsBlocked)
                throw new BadRequestException("Account is deleted or blocked.");

            var accRoles = await _userManager.GetRolesAsync(acc);
            var result = await _userManager.RemoveFromRolesAsync(acc, accRoles);
            if (!result.Succeeded)
                throw new Exception("Removing from role failed: " + result.Errors.First<IdentityError>().Description);

            result = await _userManager.AddToRoleAsync(acc, roleName);
            if (!result.Succeeded)
                throw new Exception("Adding to role failed: " + result.Errors.First<IdentityError>().Description);
        }

        /// Utility methods below

        private async Task<Account> CheckIfExistsAsync(Guid accountId)
        {
            var acc = await _userManager.FindByIdAsync(accountId.ToString());
            if (acc == null)
                throw new NotFoundException($"There is no account with the specified id.");

            return acc;
        }
    }
}
