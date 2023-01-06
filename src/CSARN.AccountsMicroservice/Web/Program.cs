using Infrastructure;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using SharedLib.AccountsMsvc.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.AppParams;
using Core.Domain.ViewModels;
using Core.Interfaces.Services;
using Core.Services;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

//Database
services.AddDbContext<AccountsContext>(opt =>
    opt.UseSqlServer(config.GetConnectionString("DatabaseConnection")));

services.Configure<JwtConfigModel>(config.GetSection("Authentication").GetSection("Jwt"));
services.Configure<JwtConfigModel>(opt =>
{
    opt.Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Authentication:Jwt:Key"]));
});

services.AddScoped<ITokensService, TokensService>();
services.AddScoped<IAccountsService, AccountsService>();
services.AddScoped<IPassportsService, PassportsService>();
services.AddScoped<IPassportsRepository, PassportsRepository>();

//Identity
services.AddIdentity<Account, IdentityRole<Guid>>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireDigit = true;
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AccountsContext>();

services.AddAuthentication(authOpt => authOpt.GetPredefinedOptions())
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOpt => jwtOpt.GetPredefinedOptions(
                        audience: config["Authentication:Jwt:Audience"],
                        issuer: config["Authentication:Jwt:Issuer"],
                        key: config["Authentication:Jwt:Key"]));
        
services.AddAuthorization(opt => opt.GetPredefinedOptions());

services.AddAutoMapper(typeof(MapperProfile));

services.AddControllers();
services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CSARN", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
