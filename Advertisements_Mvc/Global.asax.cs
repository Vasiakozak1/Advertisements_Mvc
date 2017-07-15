using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Advertisements_Mvc.Models;
using System.Configuration;
using Advertisements_Mvc.Scripts;
using System.Threading;
using System.Globalization;
namespace Advertisements_Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Languages AppLanguage = LanguageResource.ParseLanguage(ConfigurationManager.AppSettings["AppLanguage"]);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("uk");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("uk");
            }
            App_LocalResources.Layout.Culture = Thread.CurrentThread.CurrentCulture;
        }
    }
}
