using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string ContactFirtsName { get; set; }
        public string ContactLastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        //SE DEFINE LA RELACION DE MUCHOS A MUCHOS
        public virtual ICollection<SupplierProducts> SupplierProducts { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}