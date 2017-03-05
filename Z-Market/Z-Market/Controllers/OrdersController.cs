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
            Session["orderView"] = orderView;
            FullName();
            return View(orderView);
        }
        [HttpPost]
        public ActionResult NewOrder(OrderView OrderView)
        {
            FullName();
            return View(OrderView);
        }
        public ActionResult AddProduct() {
            var list = db.Products.ToList();
            list.Add(new Product { ProductID = 0, Description = "[Seleccione]" });
            list = list.OrderBy(c => c.Description).ToList();
            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            //FORMCOLLECTION TRAE TODO LO DEL FORMULARIO
            //hace  que la variable de session traiga un objeto de tipo OrderView 
            var orderView = Session["orderView"] as OrderView;
            //Request TRAE LA INFORMACION DIGITADA EN UN CONTROL DE LA VISTA
            var productID =int.Parse(Request["ProductID"]);
            if (productID==0)
            {
                var list = db.Products.ToList();
                list.Add(new Product { ProductID = 0, Description = "[Seleccione]" });
                list = list.OrderBy(c => c.Description).ToList();
                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                ViewBag.Error = "Debe seleccionar un producto";
                return View(productOrder);
            }
            var product = db.Products.Find(productID);
            if (product == null)
            {
                var list = db.Products.ToList();
                list.Add(new Product { ProductID = 0, Description = "[Seleccione]" });
                list = list.OrderBy(c => c.Description).ToList();
                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                ViewBag.Error = "El producto no existe";
                return View(productOrder);
            }
            productOrder = new ProductOrder
            {
                Description = product.Description,
                Price = product.Price,
                Quantity =float.Parse(Request["Quantity"]),
                ProductID = product.ProductID,
            };
            orderView.Products.Add(productOrder);
            return View("NewOrder",orderView);
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