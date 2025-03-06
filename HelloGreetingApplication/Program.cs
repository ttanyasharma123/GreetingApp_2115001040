using NLog;
using NLog.Web;
using BusinessLayerr.Interface;
using BusinessLayerr.Service;
using RepositoryLayerr.Service;
using RepositoryLayerr.Interface;
using Middleware_Layer.GlobalExceptionHandler; 

var builder = WebApplication.CreateBuilder(args);
var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

try
{
    logger.Info("Application is starting...");

    
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    
    builder.Services.AddControllers();

    
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();

    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

   
    app.UseMiddleware<GlobalExceptionFilter>();

    
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
