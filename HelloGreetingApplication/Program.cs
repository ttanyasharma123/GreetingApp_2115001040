using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;
using Microsoft.OpenApi.Models;
using BusinessLayerr.Interface;
using BusinessLayerr.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Application is starting...");

    var builder = WebApplication.CreateBuilder(args);

    // Add NLog to the logging pipeline
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container
    builder.Services.AddControllers();

    // Register Business Layer Services (Dependency Injection)
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();

    // Add Swagger Configuration
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline
   app.UseSwagger();
        app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Application stopped due to an exception.");
    throw;
}
finally
{
    NLog.LogManager.Shutdown(); // Ensure logs are flushed before application shutdown
}
