using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Models
{
    public class DbManager:IDisposable
    {
        private  WeatherContext _context = new WeatherContext();
        private ShortWeather _shortWeather;
        
        public DbManager()
        {
            
        }
        public DbManager(ShortWeather shortWeather)
        {
            _shortWeather = shortWeather;
        }

        public List<string> GetFavoriteCity()
        {
            var query = from city in _context.FavoriteCity
                select city.FavoriteCityName;
            return query.ToList();
        }

        public void DeleteFavoriteCity(string cityName)
        {
            var query = from city in _context.FavoriteCity
                where city.FavoriteCityName == cityName
                select city;
            _context.FavoriteCity.Remove(query.FirstOrDefault());
            _context.SaveChanges();
        }

        public void AddFavoriteCity(string cityName)
        {
            var query = from city in _context.FavoriteCity
                        where city.FavoriteCityName == cityName
                        select city;
            if (!query.Any())
            {
                _context.FavoriteCity.Add(new FavoriteCity() {FavoriteCityName = cityName});
                _context.SaveChanges();
            }
        }

        public ShortWeather GetHistoryWeather(string cityName, DateTime dateTime)
        {
            var query = (from weather in _context.ShortWeather.Include("CityWeather")
                where weather.CityWeather.CityName == cityName && weather.ListWeather.Any(e => e.Date == dateTime)
                select weather).ToList();

            ShortWeather shortWeather = new ShortWeather();
            ListWeather listWeather = new ListWeather();
            foreach (var item in query)
            {
                foreach (var listW in item.ListWeather)
                {
                    if (listW.Date == dateTime)
                    {
                        listWeather = listW;
                        break;
                    }

                }
            }
            shortWeather.ListWeather = new List<ListWeather>();
            var firstOrDefault = query.FirstOrDefault();
            if (firstOrDefault != null) shortWeather.CityWeather = firstOrDefault.CityWeather;
            shortWeather.ListWeather.Add(listWeather);
            return shortWeather;
        }

        public List<DateTime> GetDateHistoryWeather(string cityName)
        {
            DateTime dateTime = DateTime.Today;
            var query = from weather in _context.ShortWeather.Include("CityWeather")
                        where weather.CityWeather.CityName == cityName
                        select weather.ListWeather;
            List<DateTime> list = new List<DateTime>();
            foreach (var item in query)
            {
                list.AddRange(from date in item where date.Date < dateTime select date.Date);
            }
            return list;
        }  

        public void AddWeather()
        {
            if (!FindCity())
            {
                //_context.CityWeather.Add(_shortWeather.CityWeather);
                _context.ShortWeather.Add(_shortWeather);
                _context.SaveChanges();
            }
            else
            {
                bool flagChange = false;

                ShortWeather newWeather = new ShortWeather() { CityWeather = _context.CityWeather.FirstOrDefault(e => e.CityWeatheIdApi == _shortWeather.CityWeather.CityWeatheIdApi) };

                foreach (var weatherItem in _shortWeather.ListWeather)
                {
                    if (!FindWeather(weatherItem.Date, _shortWeather.CityWeather.CityWeatheIdApi))
                    {
                        flagChange = true;
                        newWeather.ListWeather.Add(weatherItem);
                    }
                }
                if (flagChange)
                {
                    _context.ShortWeather.Add(newWeather);
                    _context.SaveChanges();
                }
            }
        }

        private bool FindWeather(DateTime dateTime, int cityId)
        {
            var query = from weather in _context.ShortWeather
                where weather.CityWeather.CityWeatheIdApi == cityId && weather.ListWeather.Any(e => e.Date == dateTime)
                select weather;
            var querys = query.ToList();
            if (query.Any())
                return true;
            return false;
        }

        private bool FindCity()
        {
            var query = _context.CityWeather.Where(city => city.CityWeatheIdApi == _shortWeather.CityWeather.CityWeatheIdApi).Select(e => e).ToList();
            if (query.Any())
                return true;
            return false;
        }



        public void Dispose()
        {
            _context.Dispose();
        }


    }
}