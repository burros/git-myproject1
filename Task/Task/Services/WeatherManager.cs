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
    public enum WeatherDays
    {
        Current=1,
        Three =3,
        Seven =7
    }

    public class WeatherManager
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

        private RootObject GetDataFromApi(string nameOfCity, string keyOfApi, WeatherDays weatherDays)
        {
            try
            {
                var client = new WebClient();
                var reply =
                    client.DownloadString(string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&mode=json&units=metric&cnt={1}&APPID={2}",
                   nameOfCity, (int)weatherDays, keyOfApi));
                return JsonConvert.DeserializeObject<RootObject>(reply);
            }
            catch
            {
                throw new Exception("Exception get data from API");
            }
        }

        public WeatherManager(string nameOfCity, string keyOfApi, WeatherDays weatherDays)
        {
            WeatherData = GetDataFromApi(nameOfCity, keyOfApi, weatherDays);
        }

    }
}