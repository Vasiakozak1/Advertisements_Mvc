using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Advertisements_Mvc.Models;
using Advertisements_Mvc.Scripts;
namespace Advertisements_Mvc.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ViewResult Index()
        {
            ViewBag.CurrentTab = "Person";
            
            IEnumerable<Person> persons = HomeController.currentDB.GetAllPersons();
            ViewBag.CurrentClickedID = 1;
            return View(persons);
        }
        public ViewResult Edit(int ID)
        {
            ViewBag.InvMessage = new string[3];
            Person person = HomeController.currentDB.GetPersonBy(ID.ToString());
            return View(person);
        }
        [HttpPost]
        public ActionResult Validate(Person person)
        {
            ViewBag.InvMessage = new string[3];            
            string message;
            if (!person.IsValid(out message))
            {
                string[] errMessages = message.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                ViewBag.InvMessage = errMessages;
                return View("Edit", person);
            }
            else
                if (!UniqueChecker.IsUnique(person, out message))
            {
                string[] errMessages = message.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                ViewBag.InvMessage = errMessages;
                return View("Edit", person);
            }

            else
            {
                HomeController.currentDB.UpdatePerson(person);
                IEnumerable<Person> persons = HomeController.currentDB.GetAllPersons();
                return View("Index", persons);
            }
        }
    }
}