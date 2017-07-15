using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
namespace Advertisements_Mvc.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index(string languageAbbrevation)
        {
            if (languageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageAbbrevation);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = languageAbbrevation;
            Response.Cookies.Add(cookie);

            return Redirect("/Home/Index");
        }
        public static string GetCurrLangImg()
        {
            string abbreviation = Thread.CurrentThread.CurrentCulture.Name;
            switch (abbreviation)
            {
                case "uk":
                    return "~/Content/ukraine-flag.png";
                default: return "~/Content/unkingdom-flag.png";
            }
        }
        /// <summary>
        /// Абревіатура(en,uk) - key, шлях до картинки - value
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,string> GetAbrsAndUrls()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("en", "~/Content/unkingdom-flag.png");
            result.Add("uk", "~/Content/ukraine-flag.png");
            switch(Thread.CurrentThread.CurrentCulture.Name)
            {
                case "uk":
                    result.Remove("uk");
                    break;
                case "en":
                    result.Remove("en");
                    break;
            }
            return result;
        }
    }
}