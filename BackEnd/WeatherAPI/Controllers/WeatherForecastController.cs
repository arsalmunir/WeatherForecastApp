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
    private readonly IAuthenticationService _authenticationService;

    public WeatherForecastController(IService service, IAuthenticationService authenticationService)
    {
        _service = service;
        _authenticationService = authenticationService;

    }

    [HttpGet("weather", Name = "GetWeather")]
    public WeatherResponse GetWeather(string? city = "", string? zip = "", string? sessionID = "")
    {
        if(_authenticationService.ValidateSession(sessionID))
            return _service.GetWeather(city, zip);
        else
        {
            throw new HttpResponseException(401, "Unauthorised");
        }

    }

    [HttpGet("forecast", Name = "GetForecast")]
    public IDictionary<string, AverageForecastResponse> GetForecast(string? city = "", string? zip = "", string? sessionID = "")
    {
        if (_authenticationService.ValidateSession(sessionID))
            return _service.GetForecast(city, zip);
        else
        {
            throw new HttpResponseException(401, "Unauthorised");
        }
    }


    [HttpGet("authentication", Name = "AuthenticationProcess")]
    public string GetSesson(string? username = "", string? password = "")
    {
        return(_authenticationService.AuthenticationProcess(username, password));


    }

}