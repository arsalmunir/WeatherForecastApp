using System;
using WeatherAPI.Models.ForecastWeatherModels;
using WeatherAPI.Models.OpenWeatherMapModels;
using System.Collections.Generic;

namespace WeatherAPI.Services
{
    public interface IService
    {
        WeatherResponse GetWeather(string? city, string? zip);
        IDictionary<string, AverageForecastResponse> GetForecast(string? city, string? zip);
    }
}

