using System;
namespace WeatherAPI.Models.ForecastWeatherModels
{
    public class AverageForecastResponse
    {

        public int avgTemperature { get; set; }
        public double avgHumidity { get; set; }
        public double avgWindSpeed { get; set; }

        public AverageForecastResponse(int avgTemp, double avghumid, double avgWind)
        {
            this.avgTemperature = avgTemp;
            this.avgHumidity = avghumid;
            this.avgWindSpeed = avgWind;
        }
    }
}

