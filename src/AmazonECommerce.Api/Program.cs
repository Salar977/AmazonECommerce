using AmazonECommerce.Application;
using AmazonECommerce.Infrastructure;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
Log.Logger.Information("Application is building...");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

try
{
    var app = builder.Build();
    app.UseSerilogRequestLogging();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseInfrastructureService();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    Log.Logger.Information("Application is running...");
    app.Run();

}
catch(Exception ex)
{
    Log.Logger.Error(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}