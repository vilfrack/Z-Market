using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Z_Market.Models;
using Z_Market.ViewModels;

namespace Z_Market.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = UserManager.Users.ToList();
            var usersView = new List<UserView>();
            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Name = user.UserName,
                    UserID = user.Id
                };
                usersView.Add(userView);
            }

            return View(usersView);
        }

        public ActionResult Roles(string userID) {

            if (string.IsNullOrEmpty(userID)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var users = UserManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var user = users.Find(u => u.Id == userID);
            if (user ==null)
            {
                return HttpNotFound();
            }
            var rolesView = new List<RolView>();
            foreach (var item in user.Roles)
            {
                var rol = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RolView
                {
                    RoleID=rol.Id,
                    Name = rol.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };
          
            return View(userView);

        }

        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = UserManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };
            DropDown();
            return View(userView);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void DropDown()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            //creo una variable donde va a obtener todos los DocumentTypes
            var list = roleManager.Roles.ToList();
            //se agrega el nuevo campo que es Seleccione un tipo de documento
            list.Add(new IdentityRole { Id = "", Name = "[Seleccione un Rol]" });
            //se ordena la lista
            list = list.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(list, "Id", "Name");

        }
    }
}