using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Z_Market.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Display(Name = "Produc Description")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must between {1} and {2} characters", MinimumLength = 3)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C2}",ApplyFormatInEditMode =false)]
        [Required(ErrorMessage ="You must enter the field {0}")]
        public decimal Price { get; set; }

        [Display(Name = "Last Buy")]
        [DataType(DataType.Date)]//permite que el formato no necesite la hora
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastBuy { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public float Stock { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual ICollection<SupplierProducts> SupplierProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}