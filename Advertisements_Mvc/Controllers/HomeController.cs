using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements_Mvc.Scripts;
using Advertisements_Mvc.Models;

namespace Advertisements_Mvc.Controllers
{
    public class HomeController : Controller
    {
        public static Databasework currentDB = new Databasework();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.CurrentTab = "Home";
            return View();
        }
        public ViewResult InputFieldForSelect()
        {
            return View();
        }
        
        public ViewResult ShowAds(string field)
        {
            Advertisment adv = currentDB.GetAdvertisementBy(field);
            
            return View(adv);
        }
        /// <summary>
        /// При видаленні людини вибирається чи видалити всі оголошення
        /// які пов'язані із ним
        /// </summary>
        /// <returns></returns>
      
    }
}