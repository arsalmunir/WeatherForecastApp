using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WeatherAPI.Models.ForecastWeatherModels
{
    public class WindForecast
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }
}

