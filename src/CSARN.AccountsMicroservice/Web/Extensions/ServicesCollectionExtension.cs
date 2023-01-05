using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedLib.AccountsMsvc.Models;
using SharedLib.Auth;
using System.Text;

namespace Web.Extensions
{
    internal static class ServicesCollectionExtension
    {
        public static void AddAuth(this IServiceCollection services, ConfigurationManager configManager)
        {
            services.AddIdentity<Account, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = true;
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AccountsContext>();

            services.AddAuthentication(authOpt =>
            {
                authOpt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOpt =>
                {
                    jwtOpt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configManager["Authentication:Jwt:Audience"],
                        ValidAudience = configManager["Authentication:Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configManager["Authentication:Jwt:Key"]))
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(AccountsPolicies.DefaultRights, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(
                        AccountsRoles.Citizen,
                        AccountsRoles.Administrator,
                        AccountsRoles.MD,
                        AccountsRoles.MIA,
                        AccountsRoles.ME,
                        AccountsRoles.MES,
                        AccountsRoles.MH);
                });

                opt.AddPolicy(AccountsPolicies.StateExecutives, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(
                        AccountsRoles.Administrator,
                        AccountsRoles.MD,
                        AccountsRoles.MIA,
                        AccountsRoles.ME,
                        AccountsRoles.MES,
                        AccountsRoles.MH);
                });

                opt.AddPolicy(AccountsPolicies.Administrators, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(AccountsRoles.Administrator);
                });
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IPassportsService, PassportsService>();
        }

        public static void AddInfrastructure(this IServiceCollection services, string dbConnString)
        {
            services.AddDbContext<AccountsContext>(opt =>
                opt.UseSqlServer(dbConnString));

            services.AddScoped<IPassportsRepository, PassportsRepository>();
        }
    }
}
