using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLib.Auth;
using System.Text;

namespace Web.AppParams
{
    internal class AppParams
    {
        public static AuthenticationOptions GetAuthenticationOptions()
        {
            var opt = new AuthenticationOptions();

            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            return opt;
        }

        public static JwtBearerOptions GetJwtBearerOptions(string audience, string issuer, string key)
        {
            var opt = new JwtBearerOptions();
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
            return opt;
        }

        public static AuthorizationOptions GetAuthorizationOptions()
        {
            var opt = new AuthorizationOptions();

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

            return opt;
        }
    }
}
