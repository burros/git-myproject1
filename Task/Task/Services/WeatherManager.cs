using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Task.Models;
using System.IO;
using Newtonsoft.Json;

namespace Task.Services
{
    interface IWeatherManager
    {
        RootObject WeatherData { get; set; }
        ShortWeather ShortWeatherData { get; }
    }

    public class WeatherData
    {
        public string NameOfCity { get; set; } 
        public string KeyOfApi { get; set; } 
        public WeatherDays WeatherDays { get; set; }
    }

    public enum WeatherDays
    {
        Current = 1,
        Three = 3,
        Seven = 7
    }

    public class WeatherManager: IWeatherManager
    {
        private RootObject _weatherData;
        public RootObject WeatherData
        {
            get { return _weatherData; }
            set { _weatherData = value; }
        }
        public ShortWeather ShortWeatherData
        {
            get { return new ShortWeather(WeatherData);}
        }

        private RootObject GetDataFromApi(WeatherData weatherData)
        {
            try
            {
                var client = new WebClient();
                var reply =
                    client.DownloadString(string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&mode=json&units=metric&cnt={1}&APPID={2}",
                   weatherData.NameOfCity, (int)weatherData.WeatherDays, weatherData.KeyOfApi));
                return JsonConvert.DeserializeObject<RootObject>(reply);
            }
            catch
            {
                throw new Exception("Exception get data from API");
            }
        }

        public WeatherManager(WeatherData weatherData)
        {
            WeatherData = GetDataFromApi(weatherData);
        }

    }
}