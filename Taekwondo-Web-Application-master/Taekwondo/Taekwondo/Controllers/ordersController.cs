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
    public class ordersController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: orders

        public ActionResult Index(string product)
        {
            var prolist = new List<string>();

            var proqry = from s in db.orders.Include(o => o.product).Include(o => o.supplier)
                         orderby s.productID
                         select s.product.productname;

            prolist.AddRange(proqry.Distinct());

            ViewBag.product = new SelectList(prolist);

            var orders = from a in db.orders.Include(o => o.product).Include(o => o.supplier)
                              select a;

            if (!string.IsNullOrEmpty(product))
            {
                orders = orders.Where(x => x.product.productname == product);
            }

            return View(orders.ToList());
        }

        // GET: orders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: orders/Create
        public ActionResult Create()
        {
            ViewBag.productID = new SelectList(db.products, "productID", "productname");
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierID");
            return View();
        }

        // POST: orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderID,productID,unitprice,quantity,orderdate,supplierID")] order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productID = new SelectList(db.products, "productID", "productname", order.productID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierID", order.supplierID);
            return View(order);
        }

        // GET: orders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = new SelectList(db.products, "productID", "productname", order.productID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierID", order.supplierID);
            return View(order);
        }

        // POST: orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderID,productID,unitprice,quantity,orderdate,supplierID")] order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productID = new SelectList(db.products, "productID", "productname", order.productID);
            ViewBag.supplierID = new SelectList(db.suppliers, "supplierID", "supplierID", order.supplierID);
            return View(order);
        }

        // GET: orders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
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
