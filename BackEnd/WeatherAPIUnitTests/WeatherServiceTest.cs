using Microsoft.Extensions.Logging;
using WeatherAPI.Services;
using WeatherAPI.ExceptionHandling;
using WeatherAPI.Models.OpenWeatherMapModels;
using WeatherAPI.Models.ForecastWeatherModels;

namespace WeatherAPIUnitTests;

public class WeatherServiceTest
{

    [Fact]
    public void TestWeather()
    {
        ILoggerFactory doesntDoMuch = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
        
        var a = new Service(doesntDoMuch.CreateLogger<Service>());
        WeatherResponse resp = a.GetWeather("", "94036");


        Assert.StrictEqual<string>("Passau", resp.Name);
        Assert.StrictEqual<int>(200, resp.Cod);
        Assert.NotNull(resp);
    }

    [Fact]
    public void TestForecast()
    {
        ILoggerFactory doesntDoMuch = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();

        var a = new Service(doesntDoMuch.CreateLogger<Service>());
        IDictionary<string, AverageForecastResponse> resp = a.GetForecast("", "94036");

        Assert.NotNull(resp);
        Assert.InRange(resp.Count(), 5, 6);
        Assert.NotNull(resp.First().Value.avgHumidity);
        Assert.NotNull(resp.First().Value.avgTemperature);
        Assert.NotNull(resp.First().Value.avgWindSpeed);
    }



    [Fact]
    public void AtleastSingleParamReuired()
    {
        ILoggerFactory doesntDoMuch = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();

        var a = new Service(doesntDoMuch.CreateLogger<Service>());
        Assert.NotNull(a);
        Assert.Throws<HttpResponseException>( () => a.GetForecast(null, null));
    }

}
