using MassTransit;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Web.AppParams
{
    internal static class AppParamsExtensions
    {
        public static void GetPredefinedOptions(this SwaggerGenOptions opt)
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "CSARN", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
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
