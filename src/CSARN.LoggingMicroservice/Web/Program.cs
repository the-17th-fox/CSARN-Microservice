using Core.Constants;
using Core.Consumers;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure;
using MassTransit;
using Serilog;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

services.AddScoped<ILoggingRepository, LoggingRepository>();
services.AddScoped<ILogsService, LogsSerivce>();

services.AddMassTransit(opt =>
{
    opt.AddConsumer<NewLogsConsumer, NewLogsConsumerDefinition>();

    opt.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(configuration["MassTransit:Host"], configuration["MassTransit:VirtualHost"], hostCfg =>
        {
            hostCfg.Username(configuration["MassTransit:UserName"]);
            hostCfg.Password(configuration["MassTransit:Password"]);
        });

        cfg.ConfigureEndpoints(context);
    });
});

services.AddAutoMapper(typeof(CSARN.SharedLib.ViewModels.Pagination.PageMapperProfile<>));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalErrorsHandler>();

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