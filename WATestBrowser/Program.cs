using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WATestBrowser;

public class Program
{
    private static void Main(string[] args)
    {
        WATestBrowserHelper.StartAsync(args);
    }
}

public static class WATestBrowserHelper
{
    //public static void StartAsync(string[] args)
    //{
    //    Task.Run(() => Start(args));
    //}
        
    public static void StartAsync(string[] args)
    {
        //var builder = WebHost.CreateDefaultBuilder(args);
        var builder = WebApplication.CreateBuilder(args);
        //builder.UseKestrel(so =>
        //{
        //    so.Limits.MaxConcurrentConnections = 100;
        //    so.Limits.MaxConcurrentUpgradedConnections = 100;
        //    so.Limits.MaxRequestBodySize = 52428800;
        //});
        builder.Logging.AddServerLogger(options => { });

        var startup = new WATestBrowser.Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services);

        // Add services to the container.
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        startup.Configure(app, builder.Environment);
        //app.UseHttpsRedirection();
        

        var summaries = new[]
        {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateTime.Now.AddDays(index),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        });

        app.RunAsync();
    }
}

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}