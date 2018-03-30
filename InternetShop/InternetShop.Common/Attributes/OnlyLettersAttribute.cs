using System;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.Common.Attributes
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
