using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NerdDinner2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "UpcomingDinners",                               // Route name
                "Dinners/Page/{page}",                           // URL with params
                new { controller = "Dinners", action = "Index" } // Param defaults
            );

            routes.MapRoute(
                "Default",                                       // Route name
                "{controller}/{action}/{id}",                    // URL with params
                new { controller = "Home", action = "Index", id = "" }  // Param defaults
            );
        }
    }


}
