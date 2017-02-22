using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Z_Market.Models
{
    public enum OrderSatus
    {
        //ESTE TIPO DE CLASES LAS ENUM. PERMITE MANEJAR ESTE TIPO
        //DE CONSTANTES
        Created,
        InProgress,
        Shipped,
        Delivered
    }
}