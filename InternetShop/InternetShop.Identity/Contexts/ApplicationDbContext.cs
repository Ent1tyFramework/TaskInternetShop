using InternetShop.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InternetShop.Identity.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("IdentityDbContext") { }

        //static ApplicationDbContext()
        //{
        //    Database.SetInitializer(new DbInitializer());
        //}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    //public class DbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    //{
    //    protected override void Seed(ApplicationDbContext context)
    //    {
    //        context.Roles.Add(new IdentityRole() { Name = "User" });
    //        context.Roles.Add(new IdentityRole() { Name = "Admin" });
    //    }
    //}
}
