using System.Reflection;
using GenericHostConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .ConfigureLogging(logging =>
        logging.SetMinimumLevel(LogLevel.Trace)
    )
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddHostedService<ConsoleHostedService>()
            .AddSingleton<IWeatherService, WeatherService>();

        services.AddOptions<WeatherSettings>().Bind(hostContext.Configuration.GetSection("Weather"));
    }).UseNLog().RunConsoleAsync();
