using Microsoft.AspNet.Identity;//PARA ROLES
using Microsoft.AspNet.Identity.EntityFramework;//PARA ROLES
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
using Z_Market.Models;

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
            //SE CREA UNA INSTANCIA A LA BASE DE DATOS CLASE SECURITY 24
            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //SE CREA UN METODO PARA TRABAJAR LOS ROLES CLASE SECURITY 24
        private void CreateRoles(ApplicationDbContext db)
        {
            //PERMITE MANIPULAR LOS ROLES
            var roleManager =new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists("View"))
            {
                roleManager.Create(new IdentityRole("View"));
            }
            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }
            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }
            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }
        }
    }
}
