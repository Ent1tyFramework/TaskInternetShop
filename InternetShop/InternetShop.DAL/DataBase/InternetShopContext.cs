using InternetShop.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.DataBase
{
    public class InternetShopContext:DbContext
    {
        public InternetShopContext()
        :base("InternetShopContext")
        {
            System.Data.Entity.Database.SetInitializer<InternetShopContext>(new InternetShopInitializer());
            Database.Initialize(true);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
