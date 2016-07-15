using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Models
{
    public class ShortWeather
    {
        public int ShortWeatherId { get; set; }
        
        public ShortWeather(RootObject allWeatherInfo)
        {
            if (allWeatherInfo != null)
            {
                CityWeather = new CityWeather(){CityName = allWeatherInfo.city.name,CityWeatheIdApi = allWeatherInfo.city.id};
                ListWeather = new List<ListWeather>();
                foreach (var item in allWeatherInfo.list)
                {
                    ListWeather.Add(new ListWeather(item));
                }
            }
        }

        public ShortWeather()
        {
            ListWeather = new List<ListWeather>();
        }
        public virtual List<ListWeather> ListWeather { get; set; }
        public CityWeather CityWeather { get; set; }
    }
}