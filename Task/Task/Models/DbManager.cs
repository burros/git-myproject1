using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<string>> GetFavoriteCityAsync()
        {
            var query = from city in _context.FavoriteCity
                select city.FavoriteCityName;
            return await query.ToListAsync();
        }

        public async System.Threading.Tasks.Task DeleteFavoriteCityAsync(string cityName)
        {
            var query = from city in _context.FavoriteCity
                where city.FavoriteCityName == cityName
                select city;
            _context.FavoriteCity.Remove(await query.FirstOrDefaultAsync());
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddFavoriteCityAsync(string cityName)
        {
            var query = from city in _context.FavoriteCity
                        where city.FavoriteCityName == cityName
                        select city;
            if (!query.Any())
            {
                _context.FavoriteCity.Add(new FavoriteCity() {FavoriteCityName = cityName});
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ShortWeather> GetHistoryWeatherAsync(string cityName, DateTime dateTime)
        {
            var query = await (from weather in _context.ShortWeather.Include("CityWeather")
                where weather.CityWeather.CityName == cityName && weather.ListWeather.Any(e => e.Date == dateTime)
                select weather).ToListAsync();

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
            if (firstOrDefault != null)
                shortWeather.CityWeather = firstOrDefault.CityWeather;
            else
                return null;
            shortWeather.ListWeather.Add(listWeather);
            return shortWeather;
        }

        public async Task<List<DateTime>> GetDateHistoryWeatherAsync(string cityName)
        {
            DateTime dateTime = DateTime.Today;
            var query = await (from weather in _context.ShortWeather.Include("CityWeather")
                        where weather.CityWeather.CityName == cityName
                        select weather.ListWeather).ToListAsync();
            List<DateTime> list = new List<DateTime>();
            foreach (var item in query)
            {
                list.AddRange(from date in item where date.Date < dateTime select date.Date);
            }
            return list;
        }  

        public async System.Threading.Tasks.Task AddWeatherAsync()
        {
            if (!FindCity())
            {
                //_context.CityWeather.Add(_shortWeather.CityWeather);
                _context.ShortWeather.Add(_shortWeather);
                await _context.SaveChangesAsync();
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
                    await _context.SaveChangesAsync();
                }
            }
        }

        private bool FindWeather(DateTime dateTime, int cityId)
        {
            var query = from weather in _context.ShortWeather
                where weather.CityWeather.CityWeatheIdApi == cityId && weather.ListWeather.Any(e => e.Date == dateTime)
                select weather;
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