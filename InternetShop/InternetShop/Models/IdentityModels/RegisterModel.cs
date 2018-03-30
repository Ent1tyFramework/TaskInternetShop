using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using InternetShop.Common.Attributes;

namespace InternetShop.Models.IdentityModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Required field.")]
        [EmailAddress(ErrorMessage = "Please input correct email-address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [MinLength(4, ErrorMessage = "The login length must be at least 4 characters.")]
        [MaxLength(20, ErrorMessage = "The login length must be no longer than 20 characters.")]
        [UserName(ErrorMessage = "Invalid login.")]
        public string Login { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Required field.")]
        [OnlyLetters(ErrorMessage = "The field must consist only of the letters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Required field.")]
        [OnlyLetters(ErrorMessage = "The field must consist only of the letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required field.")]
        [MinLength(10, ErrorMessage = "The password length must be at least 10 characters.")]
        [MaxLength(30, ErrorMessage = "The password length must be no longer than 30 characters.")]
        [DataType(DataType.Password, ErrorMessage = "Insecure password.")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Required field")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}