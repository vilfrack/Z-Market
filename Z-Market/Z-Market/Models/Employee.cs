using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Z_Market.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        //Display permite visualizar el campo en la vista, si se quita el firts name saldra pegado
        [Display(Name ="Firts Name")]
        [Required(ErrorMessage ="You must enter {0}")]//el {0} copia el nombre del campo
        //El en el between {1} es el primer parametro y {2} es el segundo
        //al agregarle una cantidad maxima de caracter se le cambia el maximo chart en la base de datos
        [StringLength(30,ErrorMessage ="The field {0} must between {1} and {2} characters",MinimumLength =3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage = "The field {0} must between {1} and {2} characters", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        //ApplyFormatInEditMode es para que no guarde el formato, solo es para vista
        [DisplayFormat(DataFormatString ="{0:C2}",ApplyFormatInEditMode =false)]
        public Decimal Salary { get; set; }

        [Display(Name = "Bonus %")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public float BonusPercent { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Date)]//permite que el formato no necesite la hora
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string URL { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeID { get; set; }

        //virtual significa que esta realacionada con la otra tabla DocumentType
        public virtual DocumentType DocumentType { get; set; }
    }
}