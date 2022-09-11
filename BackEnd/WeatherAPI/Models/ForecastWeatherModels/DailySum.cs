using System;

namespace WeatherAPI.Models.ForecastWeatherModels
{
    public class DailySum
    {
        public double temperatureSum { get; set; }
        public double humiditySum { get; set; }
        public double windSpeedSum { get; set; }

        public int numberOfPeriods { get; set; }

        public DailySum(double temperatureSum, double humiditySum, double windSpeedSum, int numberOfPeriods)
        {
            this.temperatureSum = temperatureSum;
            this.humiditySum = humiditySum;
            this.windSpeedSum = windSpeedSum;
            this.numberOfPeriods = numberOfPeriods;
        }
    }
}

