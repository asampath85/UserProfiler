using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using UserProfiler.Models;

namespace UserProfiler.Controllers
{
    public class WeatherController : Controller
    {
        //
        // GET: /Weather/

        private const string OpenWeatherAPIKey = "3128cb5e2287c9b5c3e2c3eaa18db4ee";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCityWeatherDetails(string id)
        {
            string url = string.Format
                ("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&type=accurate&mode=xml&units=metric&cnt=7appid={1}",
                id, OpenWeatherAPIKey);

            using (WebClient client = new WebClient())
            {
                try
                {
                    string response = client.DownloadString(url);
                    if (!(response.Contains("message") && response.Contains("cod")))
                    {
                        XElement xEl = XElement.Load(new System.IO.StringReader(response));
                        return PartialView("WeatherDetails", GetWeatherInfo(xEl));
                    }

                }

                catch (Exception es)
                {

                }
            }

            return null;
        }


        private WeatherDetails GetWeatherInfo(XElement xEl)
        {
            IEnumerable<WeatherViewModel> w = xEl.Descendants("time").Select((el) =>
                new WeatherViewModel
                {
                    Humidity = el.Element("humidity").Attribute("value").Value + "%",
                    MaxTemperature = el.Element("temperature").Attribute("max").Value + "°",
                    MinTemperature = el.Element("temperature").Attribute("min").Value + "°",
                    Temperature = el.Element("temperature").Attribute("day").Value + "°",
                    Weather = el.Element("symbol").Attribute("name").Value,
                    WeatherDay = DayOfTheWeek(el),
                    WeatherIcon = WeatherIconPath(el),
                    WindDirection = el.Element("windDirection").Attribute("name").Value,
                    WindSpeed = el.Element("windSpeed").Attribute("mps").Value + "mps"
                });

            var weatherDetails = new WeatherDetails();
            weatherDetails.WeatherData = w.ToList();

            return weatherDetails;
        }

        private string DayOfTheWeek(XElement el)
        {
            DateTime dW = Convert.ToDateTime(el.Attribute("day").Value);
            return dW.ToShortDateString();
        }

        private string WeatherIconPath(XElement el)
        {
            string symbolVar = el.Element("symbol").Attribute("var").Value;
            string symbolNumber = el.Element("symbol").Attribute("number").Value;
            string dayOrNight = symbolVar.ElementAt(2).ToString(); // d or n
            return String.Format("~/Content/themes/base/images/WeatherIcons/{0}{1}.png", symbolNumber, dayOrNight);
        }


    }
}
