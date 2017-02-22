using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z_Market.Models;
using Z_Market.ViewModels;

namespace Z_Market.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        Z_MarketContext db = new Z_MarketContext();
        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();

            FullName();
            return View(orderView);
        }
        [HttpPost]
        public ActionResult NewOrder(OrderView OrderView)
        {
            FullName();
            return View(OrderView);
        }
        public ActionResult AddProduct(ProductOrder productOrder) {
            return View(productOrder);
        }
        private void FullName() {
            //creo una variable donde va a obtener todos los DocumentTypes
            var list = db.Customers.ToList();
            //se agrega el nuevo campo que es Seleccione un tipo de documento
            list.Add(new Customer { CustomerID = 0, FullName = "Seleccione un cliente" });
            //se ordena la lista
            list = list.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}