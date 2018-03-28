using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetShop.Models
{
    public class BasketModel
    {
        public int IDBasket { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}