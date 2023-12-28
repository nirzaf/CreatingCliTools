using Cocona;
using CreatingCliTools;
using CreatingCliTools.Weather;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = CoconaApp.CreateBuilder();

builder.Logging.AddFilter("System.Net.Http", LogLevel.Warning);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IWeatherService, OpenWeatherMapService>();

var app = builder.Build();

// app.AddCommand("weather", async (string city, IWeatherService weatherService) =>
// {
//     var weather = await weatherService.GetWeatherForCityAsync(city);
//     Console.WriteLine(JsonSerializer.Serialize(weather, new JsonSerializerOptions{WriteIndented = true}));
// });

app.AddCommands<WeatherCommands>();

app.Run();
