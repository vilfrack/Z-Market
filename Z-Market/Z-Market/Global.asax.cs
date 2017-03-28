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
            CreateSuperUser(db);
            AddPermisionToSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //METODO QUE PERMITE ASIGNAR LOS PERMISOS AL SUPER USUARIO
        private void AddPermisionToSuperUser(ApplicationDbContext db)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = UserManager.FindByName("jvillarroelcarvajal@gmail.com");
            //SE VALIDAN SI ESTE USUARIO TIENEN LOS ROLES
            //EN CASO DE NO TENERLO SE LOS ASIGNA
            if (!UserManager.IsInRole(user.Id,"View"))
            {
                UserManager.AddToRole(user.Id,"View");
            }
            if (!UserManager.IsInRole(user.Id, "Edit"))
            {
                UserManager.AddToRole(user.Id, "Edit");
            }
            if (!UserManager.IsInRole(user.Id, "Create"))
            {
                UserManager.AddToRole(user.Id, "Create");
            }
            if (!UserManager.IsInRole(user.Id, "Delete"))
            {
                UserManager.AddToRole(user.Id, "Delete");
            }
        }

        //METODO PARA CREAR UN SUPER USUARIO POR CODIGO
        private void CreateSuperUser(ApplicationDbContext db)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = UserManager.FindByName("jvillarroelcarvajal@gmail.com");
            if (user==null)
            {
                user = new ApplicationUser
                {
                    UserName = "jvillarroelcarvajal@gmail.com",
                    Email = "jvillarroelcarvajal@gmail.com"
                };
                UserManager.Create(user, "Juan123.");
            }
        }

        //SE CREA UN METODO PARA TRABAJAR LOS ROLES CLASE SECURITY 24
        //este metodo crea los roles por codigo
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
