using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models.OpenWeatherMapModels;
using WeatherAPI.Models.ForecastWeatherModels;
using WeatherAPI.Services;


namespace WeatherAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly IService _service;
    public WeatherForecastController(IService service)
    {
        _service = service;

    }

    [HttpGet("weather", Name = "GetWeather")]
    public WeatherResponse GetWeather(string? city = "", string? zip = "")
    {
        return _service.GetWeather(city, zip);
    }

    [HttpGet("forecast", Name = "GetForecast")]
    public IDictionary<string, AverageForecastResponse> GetForecast(string? city = "", string? zip = "")
    {
       return _service.GetForecast(city, zip);
    }

}