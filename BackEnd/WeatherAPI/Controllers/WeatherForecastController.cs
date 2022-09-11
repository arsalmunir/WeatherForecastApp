using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WeatherAPI.Models;
using WeatherAPI.ExceptionHandling;
using WeatherAPI.Models.OpenWeatherMapModels;
using WeatherAPI.Config;
using System.Net;
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