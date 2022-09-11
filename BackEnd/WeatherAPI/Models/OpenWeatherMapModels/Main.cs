using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models.OpenWeatherMapModels
{
    public class Main
    {
        public decimal Temp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public decimal Temp_Min { get; set; }
        public decimal Temp_Max { get; set; }

    }
}
