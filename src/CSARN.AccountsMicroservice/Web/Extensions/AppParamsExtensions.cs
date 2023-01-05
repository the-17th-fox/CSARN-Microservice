using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using SharedLib.Auth;
using System.Text;

namespace Web.AppParams
{
    internal static class AppParamsExtensions
    {
        public static void GetPredefinedOptions(this AuthenticationOptions opt)
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        public static void GetPredefinedOptions(this JwtBearerOptions opt, string audience, string issuer, string key)
        {
            opt.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        }

        public static void GetPredefinedOptions(this AuthorizationOptions opt)
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
                    AccountsRoles.MH,
                    AccountsRoles.MR);
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
                    AccountsRoles.MH,
                    AccountsRoles.MR);
            });

            opt.AddPolicy(AccountsPolicies.Administrators, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole(AccountsRoles.Administrator);
            });
        }

        public static void GetPredefinedOptions(this IBusRegistrationConfigurator opt, ConfigurationManager configManager)
        {
            opt.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configManager["MassTransit:Host"], configManager["MassTransit:VirtualHost"], hostCfg =>
                {
                    hostCfg.Username(configManager["MassTransit:UserName"]);
                    hostCfg.Password(configManager["MassTransit:Password"]);
                });

                cfg.ConfigureEndpoints(context);
            });
        }
    }
}
