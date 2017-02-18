using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public class DocumentType
    {
        [Key]
        [Display(Name = "Document Type")]
        public int DocumentTypeID { get; set; }

        [Display(Name = "Document")]
        public string Descripcion { get; set; }

        //el tipo de documento maneja una relacion de 1:n
        public ICollection<Employee> Employee { get; set; }
        public ICollection<Customer> Customer { get; set; }
    }
}