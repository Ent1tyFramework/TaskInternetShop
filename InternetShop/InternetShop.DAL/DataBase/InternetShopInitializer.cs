using InternetShop.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.DAL.DataBase
{
    public class InternetShopInitializer : DropCreateDatabaseIfModelChanges<InternetShopContext>
    {
        protected override void Seed(InternetShopContext context)
        {
            base.Seed(context);
        }
    }
}
