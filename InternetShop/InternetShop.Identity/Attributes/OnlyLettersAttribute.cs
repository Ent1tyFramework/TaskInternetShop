using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Identity.Attributes
{
    public class OnlyLettersAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                char[] charsOfValue = value.ToString().ToCharArray();
                foreach (char ch in charsOfValue)
                {
                    if (!Char.IsLetter(ch)) return false;
                }
                return true;
            }
            return false;
        }
    }
}
