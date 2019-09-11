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
    public class suppliersController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: suppliers
        public ActionResult Index( string supplierID , string email)
        {

            var emaillist = new List<string>();

            var emailqry = from s in db.suppliers
                          orderby s.email
                          select s.email;

            emaillist.AddRange(emailqry.Distinct());
            ViewBag.email = new SelectList(emaillist);

            var suppliers = from a in db.suppliers
                           select a;


            if (!string.IsNullOrEmpty(email))
            {
                suppliers = suppliers.Where(x => x.email == email);

            }
            if (!string.IsNullOrEmpty(supplierID))
            {
                suppliers = suppliers.Where(x => x.supplierID == supplierID);
              
            }

            return View(suppliers.ToList());
        }

        // GET: suppliers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "supplierID,fname,lname,email,phonenumber,address1,address2,city,province,zipcode")] supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: suppliers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "supplierID,fname,lname,email,phonenumber,address1,address2,city,province,zipcode")] supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: suppliers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            supplier supplier = db.suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            supplier supplier = db.suppliers.Find(id);
            db.suppliers.Remove(supplier);
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
