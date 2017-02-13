using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Z_Market
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //esta linea de codigo permite verificar si hay cambio en el modelo para hacer
            //la migracion clase 08
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Models.Z_MarketContext,
                Migrations.Configuration>());//se pasan dos colecciones, uno donde esta el model
            //dos donde esta el archivo de migracion
            //CON ESTE CAMBIO CADA VEZ QUE ADICIONE O QUITE CAMPO EL SISTEMA VA A FUNCIONAR
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
