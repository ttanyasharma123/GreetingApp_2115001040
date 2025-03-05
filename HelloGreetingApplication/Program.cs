using NLog;
using NLog.Web;
using BusinessLayerr.Interface;
using BusinessLayerr.Service;
using RepositoryLayerr.Service;
using RepositoryLayerr.Interface;

var builder = WebApplication.CreateBuilder(args);
var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Application is starting...");

    // Add NLog to the logging pipeline
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container
    builder.Services.AddControllers();

    // Register Business Layer Services (Dependency Injection)
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();

    builder.Services.AddScoped<IGreetingRL, GreetingRL>();

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
    NLog.LogManager.Shutdown();
}
