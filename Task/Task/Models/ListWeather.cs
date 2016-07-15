using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Models
{
    public class ListWeather
    {
        public int ListWeatherId { get; set; }
        public DateTime Date { get; set; }
        public Temp Temperature { get; set; }
        public double Pressure { get; set; }
        public int Humidity { get; set; }
        public virtual List<Weather> Weather { get; set; }

        public ListWeather(List list)
        {
            Temperature = list.temp;
            Pressure = list.pressure;
            Humidity = list.humidity;
            Weather = list.weather;
            Date = new DateTime(1970, 1, 1).AddSeconds(list.dt);
        }

        public ListWeather()
        {
        }

    }
}