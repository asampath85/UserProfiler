using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProfiler.Models
{
    public class WeatherViewModel
    {

        public string Weather { get; set; }
        public string WeatherIcon { get; set; }
        public string WeatherDay { get; set; }
        public string Temperature { get; set; }
        public string MaxTemperature { get; set; }
        public string MinTemperature { get; set; }
        public string WindDirection { get; set; }
        public string WindSpeed { get; set; }
        public string Humidity { get; set; }
    }

    public class WeatherDetails
    {
        public List<WeatherViewModel> WeatherData { get; set; }

    }
}