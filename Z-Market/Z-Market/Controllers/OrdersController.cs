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
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView;

            var customerID = int.Parse(Request["CustomerID"]);
            if (customerID == 0)
            {
                FullName();
                ViewBag.Error = "Debe seleccionar un cliente";
                return View(orderView);
            }
            var customer = db.Customers.Find(customerID);
            if (customer == null)
            {
                FullName();
                ViewBag.Error = "Cliente no existe";
                return View(orderView);
            }
            if (orderView.Products.Count==0)
            {
                FullName();
                ViewBag.Error = "Debe ingresar detalle";
                return View(orderView);
            }
            var order = new Order
            {
                CustomerID = customerID,
                DateOrder = DateTime.Now,
                OrderSatus = OrderSatus.Created
            };
            db.Orders.Add(order);
            db.SaveChanges();
            //SELECT DEL MAXIMO
            var orderID = db.Orders.ToList().Select(o => o.OrderID).Max();
            foreach (var item in orderView.Products)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID = item.ProductID,
                    Description = item.Description,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    OrderID = orderID,
                };
                //el add guarda en memoria
                db.OrderDetails.Add(orderDetail);
                //guarda en base de datos
                db.SaveChanges();
            }
          
            ViewBag.Message = string.Format("La orden: {0}, grabada ok", orderID);
            return RedirectToAction("NewOrder");
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

            productOrder = orderView.Products.Find(p =>p.ProductID==productID );
            if (productOrder==null)
            {
                productOrder = new ProductOrder
                {
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = float.Parse(Request["Quantity"]),
                    ProductID = product.ProductID,
                };
                //POR AHORA SE GUARDA EN MEMORIA
                orderView.Products.Add(productOrder);
            }
            else
            {
                //LE SUMO AL QUANTITY SI EXISTE
                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }
            
            FullName();
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