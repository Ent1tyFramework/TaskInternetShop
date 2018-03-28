using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InternetShop.Contracts.DataContracts;
using InternetShop.DAL.DataBase;

namespace InternetShop.Controllers
{
    public class BasketsController : Controller
    {
        private InternetShopContext db = new InternetShopContext();

        // GET: Baskets
        public ActionResult Index()
        {
            return View(db.Baskets.ToList());
        }

        // GET: Baskets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = db.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // GET: Baskets/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Baskets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Basket basket)
        {
            if (ModelState.IsValid)
            {
                db.Baskets.Add(basket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(basket);
        }

        // GET: Baskets/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = db.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // POST: Baskets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Basket basket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(basket).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View("Edit", basket);
        }

        // GET: Baskets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Basket basket = db.Baskets.Find(id);
            if (basket == null)
            {
                return HttpNotFound();
            }
            return View(basket);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Basket basket = db.Baskets.Find(id);
            db.Baskets.Remove(basket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Ordering(int IDUser)
        {
            IDUser = 1;
            for (int i = 0; i < db.Baskets.Where(x=>x.Users==db.Users.Where(t=>t.IDUser==IDUser)).Count(); i++)
            {

                db.Orders.Add(new Order { Count = ((db.Baskets.First(x => x.Users == db.Users.Where(t => t.IDUser == IDUser))).Count),
                    Products = ((db.Baskets.First(x => x.Users == db.Users.Where(t => t.IDUser == IDUser))).Products),
                    Status = "Готовится к выполнению",
                    Date = DateTime.Now ,
                    Users = db.Users.Where(x=>x.IDUser==IDUser).ToList(),
                    Address = db.Users.First(x=>x.IDUser==IDUser).Address
                });
                db.Baskets.Remove(db.Baskets.First(x=>x.Users == db.Users.Where(t=>t.IDUser==IDUser)));

            }



            
            return RedirectToAction("Index");
        }
    }
}
