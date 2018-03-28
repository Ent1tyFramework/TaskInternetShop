using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetShop.Models.IdentityModels
{
    public class ResetPasswordModel
    {
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "Required field.")]
        [MinLength(10, ErrorMessage = "The password length must be at least 10 characters.")]
        [MaxLength(30, ErrorMessage = "The password length must be no longer than 30 characters.")]
        [DataType(DataType.Password, ErrorMessage = "Insecure password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string UserId { get; set; }

        public string Token { get; set; }
    }
}