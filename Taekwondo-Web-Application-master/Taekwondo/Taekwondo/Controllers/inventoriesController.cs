using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Taekwondo.Models;

namespace Taekwondo.Controllers
{
    public class inventoriesController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: inventories
        public ActionResult Index(string product)
        {
            var prolist = new List<string>();

            var proqry = from s in db.inventories.Include(i => i.product).Include(i => i.supplier)
                         orderby s.productID
                          select s.product.productname;

            prolist.AddRange(proqry.Distinct());

            ViewBag.product = new SelectList(prolist);

            var inventories = from a in db.inventories.Include(i => i.product).Include(i => i.supplier)
                              select a;

            if (!string.IsNullOrEmpty(product))
            {
                inventories = inventories.Where(x =>x.product.productname == product);
            }

            return View(inventories.ToList());
        }

        // GET: inventories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inventory inventory = db.inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: inventories/Create
        public ActionResult Create()
        {
            ViewBag.productID = new SelectList(db.products, "productID", "productname");
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "fname");
            return View();
        }

        // POST: inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "inventoryID,productID,costprice,sellingprice,profit,quantityinstock,supplierID")] inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productID = new SelectList(db.products, "productID", "productname", inventory.productID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "fname", inventory.supplierID);
            return View(inventory);
        }

        // GET: inventories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inventory inventory = db.inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = new SelectList(db.products, "productID", "productname", inventory.productID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "fname", inventory.supplierID);
            return View(inventory);
        }

        // POST: inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "inventoryID,productID,costprice,sellingprice,profit,quantityinstock,supplierID")] inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productID = new SelectList(db.products, "productID", "productname", inventory.productID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "fname", inventory.supplierID);
            return View(inventory);
        }

        // GET: inventories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inventory inventory = db.inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            inventory inventory = db.inventories.Find(id);
            db.inventories.Remove(inventory);
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
    }
}
