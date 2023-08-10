using WorkerServicePractice;
using Serilog;
using Serilog.Events;
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File("C:/Users/makha/OneDrive/Desktop/Logger.txt")
    .CreateLogger();
try
{
    Log.Information("Starting up the service");
    host.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "There was a problem starting up the service");
}
finally
{
    Log.CloseAndFlush();
}