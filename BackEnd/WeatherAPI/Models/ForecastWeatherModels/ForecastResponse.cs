using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models.ForecastWeatherModels
{
    public class ForecastResponse
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<WeeklyForecast> list { get; set; }
        public CityForecast city { get; set; }
    }
}

