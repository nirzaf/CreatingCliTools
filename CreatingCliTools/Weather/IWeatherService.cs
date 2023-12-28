using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace CreatingCliTools.Weather;

public interface IWeatherService
{
    Task<OpenWeatherMapService.WeatherResponse?> GetWeatherForCityAsync(string city);
}

public class OpenWeatherMapService(IHttpClientFactory httpClientFactory) : IWeatherService
{
    private const string ApiKey = "47232a8e5b8e1b4189713ee6ff0520da";

    public async Task<WeatherResponse?> GetWeatherForCityAsync(string city)
    {
        var client = httpClientFactory.CreateClient();

        var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}&units=metric";

        var weatherResponse = await client.GetAsync(url);
        if (weatherResponse.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        return await weatherResponse.Content.ReadFromJsonAsync<WeatherResponse>();
    }

    public class WeatherResponse
    {
        [JsonPropertyName("main")] public OpenWeatherMapWeather Weather { get; set; }

        [JsonPropertyName("visibility")] public int Visibility { get; set; }

        [JsonPropertyName("dt")] public int Dt { get; set; }

        [JsonPropertyName("timezone")] public int Timezone { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("cod")] public int Cod { get; set; }
    }

    public class OpenWeatherMapWeather
    {
        [JsonPropertyName("temp")] public double Temp { get; set; }

        [JsonPropertyName("feels_like")] public double FeelsLike { get; set; }

        [JsonPropertyName("temp_min")] public double TempMin { get; set; }

        [JsonPropertyName("temp_max")] public double TempMax { get; set; }

        [JsonPropertyName("pressure")] public int Pressure { get; set; }

        [JsonPropertyName("humidity")] public int Humidity { get; set; }
    }
}
