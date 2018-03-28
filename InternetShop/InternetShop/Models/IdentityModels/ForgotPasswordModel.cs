using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetShop.Models.IdentityModels
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Required field.")]
        [EmailAddress(ErrorMessage = "Please input correct email-address.")]
        public string Email { get; set; }
    }
}