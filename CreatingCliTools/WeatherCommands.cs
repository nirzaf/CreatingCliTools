using System.Text.Json;
using Cocona;
using CreatingCliTools.Weather;

namespace CreatingCliTools;

public class WeatherCommands(IWeatherService weatherService)
{
    [Command("weather")]
    public async Task Weather1(string city)
    {
        var weather = await weatherService.GetWeatherForCityAsync(city);
        Console.WriteLine(JsonSerializer.Serialize(weather, new JsonSerializerOptions{WriteIndented = true}));
    }
    
    [Ignore]
    public async Task Weather2(string city)
    {
        var weather = await weatherService.GetWeatherForCityAsync(city);
        Console.WriteLine(JsonSerializer.Serialize(weather, new JsonSerializerOptions{WriteIndented = true}));
    }
}
