using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Advertisements_Mvc.Models;
using System.Configuration;
using Advertisements_Mvc.Scripts;
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
    }
}
