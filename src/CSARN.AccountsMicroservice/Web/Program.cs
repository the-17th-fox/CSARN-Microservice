using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web.AppParams;
using Core.Domain.ViewModels;
using MassTransit;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<JwtConfigModel>(configuration.GetSection("Authentication").GetSection("Jwt"));
services.Configure<JwtConfigModel>(opt =>
{
    opt.Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Jwt:Key"]));
});

services.AddMassTransit(opt => opt.GetPredefinedOptions(configuration));

services.AddAuth(configuration);
services.AddInfrastructure(configuration.GetConnectionString("DatabaseConnection"));
services.AddServices();

services.AddAutoMapper(typeof(MapperProfile));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options => options.GetPredefinedOptions());

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
