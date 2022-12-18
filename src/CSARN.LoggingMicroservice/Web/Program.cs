using Core.Constants;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

services.AddScoped<ILoggingRepository, ILoggingRepository>();
services.AddScoped<ILogsService, LogsSerivce>();

services.AddAutoMapper(typeof(CSARN.SharedLib.ViewModels.Pagination.PageMapperProfile<>));

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

Log.Information(LogEvents.MicroserviceIsAboutToRun, "LoggingMsvc");
app.Run();