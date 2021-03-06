﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Models
{


    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Temp temp { get; set; }
        public double pressure { get; set; }
        public int humidity { get; set; }
        public List<Weather> weather { get; set; }
    }

    public class RootObject
    {
        public string cod { get; set; }
        public double message { get; set; }
        public City city { get; set; }
        public int cnt { get; set; }
        public List<List> list { get; set; }
    }


    public class ShortWeather
    {
        public string NameOfCity { get; set; }
        public List<ListWeather> ListWeather { get; set; }
        public ShortWeather(RootObject allWeatherInfo)
        {
            if (allWeatherInfo != null)
            {
                NameOfCity = allWeatherInfo.city.name;
                ListWeather = new List<ListWeather>();
                foreach(var item in allWeatherInfo.list)
                {
                    ListWeather.Add(new ListWeather(item));
                }
            }
        }

    }

    public class ListWeather
    {
        public DateTime Date { get; set; }
        public Temp Temperature { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        public List<Weather> Weather { get; set; }

        public ListWeather(List list)
        {
            Temperature = list.temp;
            Pressure = list.pressure;
            Humidity = list.humidity;
            Weather = list.weather;
            Date = new DateTime(1970, 1, 1).AddSeconds(list.dt);
        }

    }

}