using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class SupplierProducts
    {
        [Key]
        public int SupplierProductsID { get; set; }
        public int SupplierID { get; set; }
        public int ProductID { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Product Product { get; set; }
    }
}