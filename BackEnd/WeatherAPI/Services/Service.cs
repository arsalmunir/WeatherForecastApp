using System;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using System.Xml.Linq;
using WeatherAPI.Models.OpenWeatherMapModels;
using WeatherAPI.ExceptionHandling;
using WeatherAPI.Models.ForecastWeatherModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;


namespace WeatherAPI.Services
{
    public class Service : IService
    {
        private static readonly int kelvin = -273;
        private readonly ILogger<Service> _logger;
        private readonly RestClient _client;

        public Service(ILogger<Service> logger)
        {
            _logger = logger;
            _client = new RestClient("https://api.openweathermap.org/");
        }

        public WeatherResponse GetWeather(string? city = "", string? zip = "")
        {
            var response = _client.ExecuteGet(buildRequest("data/2.5/weather?", city, zip));
            if (response.IsSuccessful)
            {
                var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                weatherResponse.Main.Temp = Math.Round((weatherResponse.Main.Temp + kelvin), 0, MidpointRounding.ToEven);
                weatherResponse.Main.Temp_Min = Math.Round(weatherResponse.Main.Temp_Min + kelvin, 0, MidpointRounding.ToEven);
                weatherResponse.Main.Temp_Max = Math.Round(weatherResponse.Main.Temp_Max + kelvin, 0, MidpointRounding.ToEven);
                return weatherResponse;
            }
            else
            {
                _logger.LogError("Error response from OpenWeather API. URI: {}, ErrorException: {}", response.ResponseUri, response.ErrorException);
                throw new HttpResponseException(((int)response.StatusCode), response.Content);
            }
        }

        public IDictionary<string, AverageForecastResponse> GetForecast(string? city = "", string? zip = "")
        {
            var response = _client.ExecuteGet(buildRequest("data/2.5/forecast?", city, zip));
            if (response.IsSuccessful)
            {
                var weatherDetails = JsonSerializer.Deserialize<ForecastResponse>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                IDictionary<string, AverageForecastResponse>  a = averageForecast(weatherDetails.list);
                _logger.LogInformation("count: {}", a.Count());
                return a;
            }
            else
            {
                _logger.LogError("Error response from OpenWeather API. URI: {}, ErrorException: {}", response.ResponseUri, response.ErrorException);
                throw new HttpResponseException(((int)response.StatusCode), response.Content);
            }

        }

        private RestRequest buildRequest(string? baseUri = "", string? city = "", string? zip = "") {

            if (city != "")
            {
                baseUri += $"q={city}";
            }
            else if (zip != "")
            {
                baseUri += $"zip={zip}" + ",de";
            }
            else
            {
                _logger.LogTrace("Neither city nor zip query parameter is provided");
                throw new HttpResponseException((400), "{\"cod\":\"400\",\"message\":\"Either city or zip query parameter must be provided\"}");
            }

            baseUri += $"&appid={Config.Constants.OPEN_WEATHER_APPID}";
            return new RestRequest(baseUri);
        }


        private IDictionary<string, AverageForecastResponse> averageForecast(List<WeeklyForecast> weeklyForecasts)
        {
            
            IDictionary<string, DailySum> weatherDailySums = new Dictionary<string, DailySum>();

            foreach (WeeklyForecast dailyForecast in weeklyForecasts)
            {

                string date = dailyForecast.dt_txt.Split(" ")[0];

                if (!weatherDailySums.ContainsKey(date))
                {
                    weatherDailySums.Add(date, new DailySum(dailyForecast.main.temp, dailyForecast.main.humidity, dailyForecast.wind.speed, 1));
                }
                else
                {
                    DailySum dailySum = weatherDailySums[date];
                    dailySum.temperatureSum += dailyForecast.main.temp;
                    dailySum.humiditySum += dailyForecast.main.humidity;
                    dailySum.windSpeedSum += dailyForecast.wind.speed;
                    dailySum.numberOfPeriods++;
                    weatherDailySums[date] = dailySum;
                }
            }

            IDictionary<string, AverageForecastResponse> avgForecastDictionary = new Dictionary<string, AverageForecastResponse>();

            foreach (KeyValuePair<string, DailySum> kvp in weatherDailySums)
            {
                avgForecastDictionary.Add(kvp.Key, new AverageForecastResponse(
                    (int) Math.Round(((kvp.Value.temperatureSum / kvp.Value.numberOfPeriods) + kelvin), 0, MidpointRounding.ToEven), 
                    Math.Round((kvp.Value.humiditySum / kvp.Value.numberOfPeriods), 2),
                    Math.Round((kvp.Value.windSpeedSum / kvp.Value.numberOfPeriods), 2)));
            }
            return avgForecastDictionary;
        }


    }

}