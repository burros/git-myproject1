using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Task.Models;
using Task.Services;

namespace Task.Controllers
{
    public class HomeController : Controller
    {
        private IWeatherManager _weatherManager;

        [HttpGet]
        public async Task<ActionResult> Index(string city = "Lviv", string day = "current")
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
            await _weatherManager.GetDataFromApiAsync();
            var shortWeatherData =  _weatherManager.ShortWeatherData();
            if (shortWeatherData != null)
            {
                DbManager dbManager = new DbManager(shortWeatherData);
                await dbManager.AddWeatherAsync();
                ViewBag.FavoriteCityList = await dbManager.GetFavoriteCityAsync();
            }

            return View(shortWeatherData);
        }

       
        public async Task<ActionResult> AddCity(string city)
        {
            DbManager dbManager = new DbManager();
            await dbManager.AddFavoriteCityAsync(city);
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> DeleteCity(string city, string day)
        {
            DbManager dbManager = new DbManager();
            await dbManager.DeleteFavoriteCityAsync(city);
            return RedirectToAction("Index");
        }


    }
}