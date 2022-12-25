using Core.Domain.ViewModels.Accounts;
using Core.Interfaces.Services;
using Core.Utilities;
using Core.ViewModels.Accounts;
using CSARN.SharedLib.Constants.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLib.AccountsMsvc.Misc;
using SharedLib.AccountsMsvc.Models;
using SharedLib.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IPassportsService _passSvc;
        private readonly IOptions<JwtConfigModel> _jwtConfig;

        public AccountsService(UserManager<Account> userManager, IPassportsService passSvc, IOptions<JwtConfigModel> jwtConfig)
        {
            _userManager = userManager;
            _passSvc = passSvc;
            _jwtConfig = jwtConfig;
        }

        public async Task BlockAsync(Guid id)
        {
            var acc = await _userManager.FindByIdAsync(id.ToString());
            if (acc == null)
                throw new NotFoundException($"There is no account with the specified id.");

            if (acc.IsBlocked)
                throw new BadRequestException("Account has been already blocked.");

            acc.IsBlocked = true;

            if (!(await _userManager.UpdateAsync(acc)).Succeeded)
                throw new Exception("Updating account's blocked status has failed.");
        }

        public async Task UnblockAsyc(Guid id)
        {
            var acc = await _userManager.FindByIdAsync(id.ToString());
            if (acc == null)
                throw new NotFoundException($"There is no account with the specified id.");

            if (!acc.IsBlocked)
                throw new BadRequestException("Account isn't blocked.");

            acc.IsBlocked = false;

            if (!(await _userManager.UpdateAsync(acc)).Succeeded)
                throw new Exception("Updating account's blocked status has failed.");
        }

        public async Task CreateAsync(RegistrationParametersModel regParams)
        {
            if (regParams.PassRegionCode == SharedLib.AccountsMsvc.Misc.RegionCodes.Undefined)
                throw new InvalidParamsException("Region code is undefiend.");

            var passport = new Passport()
            {
                FirstName = regParams.FirstName,
                LastName = regParams.LastName,
                Patronymic = regParams.Patronymic,
                Region = Enum.GetName<RegionCodes>(regParams.PassRegionCode) ?? throw new Exception("Can not get the name of the region"),
                Number = regParams.PassNumber
            };

            var acc = new Account()
            { 
                Passport = passport,
                UserName = regParams.Email,
                PhoneNumber = regParams.PhoneNumber,
                Email = regParams.Email
            };

            var result = await _userManager.CreateAsync(acc, regParams.Password);
            if (!result.Succeeded)
                throw new Exception("User creation failed: " + result.Errors.First<IdentityError>().Description);

            result = await _userManager.AddToRoleAsync(acc, AccountsRoles.Citizen);
            if (!result.Succeeded)
                throw new Exception("Adding to role failed: " + result.Errors.First<IdentityError>().Description);
        }

        public async Task DeleteAsync(Guid id)
        {
            var acc = await _userManager.FindByIdAsync(id.ToString());
            if (acc == null)
                throw new NotFoundException($"There is no account with the specified id.");

            if (acc.IsDeleted)
                throw new BadRequestException("Account has been already deleted.");

            acc.IsDeleted = true;

            if (!(await _userManager.UpdateAsync(acc)).Succeeded)
                throw new Exception("Account deleting has failed.");
        }

        public Task<List<Account>> GetAllAsync(AccPaginationViewModel pageParams)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            var acc = await _userManager.FindByIdAsync(id.ToString());
            if (acc == null)
                throw new NotFoundException($"There is no account with the specified id.");

            return acc;
        }

        public async Task<string> LoginAsync(LoginViewModel loginViewModel)
        {
            var acc = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (acc == null)
                throw new NotFoundException($"There is no account with the specified email.");

            if (acc.IsDeleted || acc.IsBlocked)
                throw new BadRequestException("Account is deleted or blocked.");

            var isCorrect = await _userManager.CheckPasswordAsync(acc, loginViewModel.Password);
            if (!isCorrect)
                throw new InvalidParamsException("Password check has failed.");

            var accRoles = await _userManager.GetRolesAsync(acc);
            var userClaims = GetClaims(acc, accRoles);
            var token = CreateSecurityToken(userClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetClaims(Account account, IList<string> accountRoles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
            };

            foreach (var role in accountRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken CreateSecurityToken(List<Claim> claims)
        {
            var symSecurityKey = _jwtConfig.Value.Key;
            return new(
                issuer: _jwtConfig.Value.Issuer,
                audience: _jwtConfig.Value.Audience,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtConfig.Value.LifetimeHours),
                signingCredentials: new SigningCredentials(symSecurityKey, SecurityAlgorithms.HmacSha256));
        }

        public async Task ChangeRoleAsync(Guid id, string roleName)
        {
            var acc = await _userManager.FindByIdAsync(id.ToString());
            if (acc == null)
                throw new NotFoundException($"There is no account with the specified id.");

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
    }
}
