using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Mictlanix.Iam.Models;

namespace Mictlanix.Iam
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ArrangementsCreate", // Route name
                "Arrangements/CreateFromRequest/{id}", // URL with parameters
                new { controller = "Arrangements", action = "CreateFromRequest" } // Parameter defaults
            );

            routes.MapRoute(
                "Arrangements", // Route name
                "Arrangements/{action}/{year}/{serial}", // URL with parameters
                new { controller = "Arrangements", action = "Index", year = UrlParameter.Optional, serial = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //Database.SetInitializer<SSContext>(new DropCreateDatabaseIfModelChanges<SSContext>());
            //new SSContext().Database.Create();
            //new SSContext().Database.ExecuteSqlCommand("INSERT INTO Users VALUES ('admin', '7C4A8D09CA3762AF61E59520943DC26494F8941B', 'Eddy', 'Zavaleta', 'eddy@mictlanix.org', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)");
        }
    }
}