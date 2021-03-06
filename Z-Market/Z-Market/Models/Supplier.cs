﻿using System;
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

        [StringLength(30,ErrorMessage ="The fiel {0} must contain between {2} and {1} characters", MinimumLength =3)]
        [Required(ErrorMessage ="You must enter the field {0}")]
        [Display(Name ="Suplier Name")]
        public string Name { get; set; }

        [StringLength(30, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Contact Firts Name")]
        public string ContactFirtsName { get; set; }

        [StringLength(30, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Contact Last Name")]
        public string ContactLastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(30, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [StringLength(30, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //SE DEFINE LA RELACION DE MUCHOS A MUCHOS
        public virtual ICollection<SupplierProducts> SupplierProducts { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}