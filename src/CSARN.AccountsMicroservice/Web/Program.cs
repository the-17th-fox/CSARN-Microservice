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

services.AddAuthentication(options => AppParams.GetAuthenticationOptions())
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt => AppParams.GetJwtBearerOptions(
                        audience: config["Authentication:Jwt:Audience"],
                        issuer: config["Authentication:Jwt:Issuer"],
                        key: config["Authentication:Jwt:Key"]));
        
services.AddAuthorization(opt => AppParams.GetAuthorizationOptions());

services.AddAutoMapper(typeof(MapperProfile));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
