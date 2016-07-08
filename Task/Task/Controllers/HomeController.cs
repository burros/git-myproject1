using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Services;

namespace Task.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(string city = "Lviv", string day = "current")
        {
            if (string.IsNullOrEmpty(city))
                city = "Lviv";
            if(string.IsNullOrEmpty(day))
                day = "current";
            var configuration = System.Web.Configuration.WebConfigurationManager.AppSettings;
            WeatherManager weatherManager;
            ViewBag.NameOfCity = city;
            switch (day)
            {
                case "current":
                    weatherManager = new WeatherManager(city, configuration["KeyOfAPI"], WeatherDays.Current);
                    ViewBag.CurrentSelected = true;
                    break;
                case "three":
                    weatherManager = new WeatherManager(city, configuration["KeyOfAPI"], WeatherDays.Three);
                    ViewBag.ThreeSelected = true;
                    break;
                case "seven":
                    weatherManager = new WeatherManager(city, configuration["KeyOfAPI"], WeatherDays.Seven);
                    ViewBag.SevenSelected = true;
                    break;
                default:
                    weatherManager = new WeatherManager(city, configuration["KeyOfAPI"], WeatherDays.Current);
                    ViewBag.CurrentSelected = true;
                    break;
            }
            return View(weatherManager.ShortWeatherData);
        }


    }
}