using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetShop.Models
{
    public class ProductModel
    {
        public int IDProduct { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
       
    }
}