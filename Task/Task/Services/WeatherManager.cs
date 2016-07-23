using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Task.Models;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace Task.Services
{
    interface IWeatherManager
    {
        RootObject WeatherData { get; set; }
        ShortWeather ShortWeatherData();
        System.Threading.Tasks.Task GetDataFromApiAsync();

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
        private WeatherData _data;
        private RootObject _weatherData;
        public RootObject WeatherData
        {
            get { return _weatherData; }
            set { _weatherData = value; }
        }
        public  ShortWeather ShortWeatherData()
        {
            //GetDataFromApi();
            if (WeatherData == null)
                return null;
            return new ShortWeather(WeatherData);
            
        }

        public async System.Threading.Tasks.Task GetDataFromApiAsync()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var reply = await
                    client.DownloadStringTaskAsync(string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&mode=json&units=metric&cnt={1}&APPID={2}",
                        _data.NameOfCity, (int)_data.WeatherDays, _data.KeyOfApi));
                    var result = await JsonConvert.DeserializeObjectAsync<RootObject>(reply);
                    WeatherData = result;
                }
            }
            catch
            {
                throw new Exception("Exception get data from API");
            }
        }

        public WeatherManager(WeatherData weatherData)
        {
            _data = weatherData;
        }

    }
}