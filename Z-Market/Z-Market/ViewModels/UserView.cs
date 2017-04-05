using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Z_Market.ViewModels;

namespace Z_Market.Models
{
    public class UserView
    {
        public string UserID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //sirve para pintar los roles en la vista
        public RolView Role { get; set; }

        public List<RolView> Roles { get; set; }
    }
}