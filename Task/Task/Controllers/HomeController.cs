using Ninject;
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
        private IWeatherManager _weatherManager;

        [HttpGet]
        public ActionResult Index(string city = "Lviv", string day = "current")
        {
            IKernel ninjectKernel = new StandardKernel();
            
            if (string.IsNullOrEmpty(city))
                city = "Lviv";
            if(string.IsNullOrEmpty(day))
                day = "current";
            var configuration = System.Web.Configuration.WebConfigurationManager.AppSettings;
            ViewBag.NameOfCity = city;
            switch (day)
            {
                case "current":
                    ninjectKernel.Bind<IWeatherManager>().To<WeatherManager>().WithConstructorArgument("weatherData", new WeatherData { NameOfCity = city, KeyOfApi = configuration["KeyOfAPI"], WeatherDays = WeatherDays.Current });
                    ViewBag.CurrentSelected = true;
                    break;
                case "three":
                    ninjectKernel.Bind<IWeatherManager>().To<WeatherManager>().WithConstructorArgument("weatherData", new WeatherData { NameOfCity = city, KeyOfApi = configuration["KeyOfAPI"], WeatherDays = WeatherDays.Three });
                    ViewBag.ThreeSelected = true;
                    break;
                case "seven":
                    ninjectKernel.Bind<IWeatherManager>().To<WeatherManager>().WithConstructorArgument("weatherData", new WeatherData { NameOfCity = city, KeyOfApi = configuration["KeyOfAPI"], WeatherDays = WeatherDays.Seven });
                    ViewBag.SevenSelected = true;
                    break;
                default:
                    ninjectKernel.Bind<IWeatherManager>().To<WeatherManager>().WithConstructorArgument("weatherData", new WeatherData { NameOfCity = city, KeyOfApi = configuration["KeyOfAPI"], WeatherDays = WeatherDays.Current });
                    ViewBag.CurrentSelected = true;
                    break;
            }
            _weatherManager = ninjectKernel.Get<IWeatherManager>();
            return View(_weatherManager.ShortWeatherData);
        }


    }
}