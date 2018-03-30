using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Common.Attributes
{
    public class UserNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                char[] allow_chars = "qwertyuiopasdfghjklzxcvbnm0123456789-_".ToCharArray();

                char[] charsOfValue = value.ToString().ToCharArray();
                foreach (char ch in charsOfValue)
                {
                    if (!allow_chars.Contains(ch)) return false;
                }
                return true;
            }
            return false;
        }
    }
}
