using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WeatherAPI.Models.ForecastWeatherModels
{
    public class MainForecast
    {

        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int sea_level { get; set; }
        public int gmd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }

    }
}

