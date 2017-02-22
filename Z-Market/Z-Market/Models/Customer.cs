using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(30, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Firts Name")]
        public string FirtsName { get; set; }

        [StringLength(30, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

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

        [StringLength(30, ErrorMessage = "The fiel {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Document")]
        public string Document { get; set; }

        public int DocumentTypeID { get; set; }
        public string _FullName;
        [NotMapped]
        public string FullName {
            get {
                _FullName = string.Format("{0} {1}", FirtsName, LastName);
                return _FullName;
            }
            set { _FullName = value; }
        }

        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}