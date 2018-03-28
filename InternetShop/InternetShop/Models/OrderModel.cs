using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetShop.Models
{
    public class OrderModel
    {
        public int IDOrder { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
      
    }
}