using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WeatherAPI.Models.ForecastWeatherModels
{
    public class WeeklyForecast
    {
        public int dt { get; set; }
        public MainForecast main { get; set; }
        public List<WeatherForecast> weather { get; set; }
        public CloudsForecast clouds { get; set; }
        public WindForecast wind { get; set; }
        public int visibility { get; set; }
        public double pop { get; set; }
        public RainForecast rain { get; set; }
        public SysForecast sys { get; set; }
        public string dt_txt { get; set; }

    }
}
