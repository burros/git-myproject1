using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;

namespace Task.Controllers
{
    public class HistoryController : Controller
    {
        //
        // GET: /History/
        public ActionResult Index(string city = "Lviv", string date = null)
        {
            if (string.IsNullOrEmpty(city))
                city = "Lviv";
            if(string.IsNullOrEmpty(date))
                date = DateTime.Now.ToLongDateString();
            DbManager dbManager = new DbManager();

            ViewBag.HistoryDate = dbManager.GetDateHistoryWeather(city);
            ViewBag.CityName = city;
            return View(dbManager.GetHistoryWeather(city, DateTime.Parse(date).AddHours(10)));
           
        }
	}
}