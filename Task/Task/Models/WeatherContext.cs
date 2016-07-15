using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Task.Models
{
    public class WeatherContext:DbContext
    {
        public WeatherContext()
            : base("WeatherContext")
        { }
        public DbSet<ShortWeather> ShortWeather { get; set; }
        public DbSet<ListWeather> ListWeathers { get; set; }
        public DbSet<Weather> Weather { get; set; }
        public DbSet<CityWeather> CityWeather { get; set; }
        public DbSet<FavoriteCity> FavoriteCity { get; set; }
        

    }
}