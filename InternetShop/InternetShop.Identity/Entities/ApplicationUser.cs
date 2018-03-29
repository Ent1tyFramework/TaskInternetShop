using InternetShop.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var claimsIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            claimsIdentity.AddClaims(new List<Claim>() {
            new Claim("FirstName", FirstName),
            new Claim("LastName", LastName)
            });

            return claimsIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
