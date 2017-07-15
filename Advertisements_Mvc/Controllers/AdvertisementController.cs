using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements_Mvc.Models;
namespace Advertisements_Mvc.Controllers
{
    public class AdvertisementController : Controller
    {
        // GET: Advertisement
        public ActionResult Index()
        {
            ViewBag.CurrentTab = "Advertisement";
            IEnumerable<Advertisment> advertisements = HomeController.currentDB.GetAllAdvertisements();
            return View(advertisements);
        }
        public ActionResult Create()
        {
            ViewBag.CurrentTab = "Advertisement";
            SelectListItem itm1 = new SelectListItem();
            SelectListItem itm2 = new SelectListItem();
            SelectListItem itm3 = new SelectListItem();
            SelectListItem itm4 = new SelectListItem();
            itm1.Text = "Медицина"; itm1.Value = "Медицина";
            itm2.Text = "Навчання"; itm2.Value = "Навчання";
            itm3.Text = "Комерція"; itm3.Value = "Комерція";
            itm4.Text = "Знайомства"; itm4.Value = "Знайомства";
            SelectListItem[] advTypes = new SelectListItem[] { itm1, itm2, itm3, itm4 };
            ViewBag.advTypes = advTypes;
            return View();
        }
        [HttpPost]
        public ActionResult AcceptAdding(Advertisment advertisement)
        {

            return Index();
        }
    }
}