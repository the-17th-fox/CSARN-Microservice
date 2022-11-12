using DomainServiceAbstractions;
using Infrastructure.Repositories;
using Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.Configure<RepositoryConfiguration>(config.GetSection("ConnectionStrings"));

services.AddScoped<IClassificationsRepository, ClassificationsRepository>();
services.AddScoped<IRepliesRepository, RepliesRepository>();
services.AddScoped<IReportsRepository, ReportsRepository>();
services.AddScoped<INotificationsRepository, NotificationsRepository>();

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
