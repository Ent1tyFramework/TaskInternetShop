using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetShop.Models
{
    public class UserModel
    {
        public int IDUser { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDay { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}