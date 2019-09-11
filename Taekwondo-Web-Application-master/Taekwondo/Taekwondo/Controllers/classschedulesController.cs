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
    public class classschedulesController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: classschedules
        public ActionResult Index()
        {
            return View(db.classschedules.ToList());
        }

        // GET: classschedules/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            classschedule classschedule = db.classschedules.Find(id);
            if (classschedule == null)
            {
                return HttpNotFound();
            }
            return View(classschedule);
        }

        // GET: classschedules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: classschedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "classID,classlevel,classday,classtimings")] classschedule classschedule)
        {
            if (ModelState.IsValid)
            {
                db.classschedules.Add(classschedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classschedule);
        }

        // GET: classschedules/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            classschedule classschedule = db.classschedules.Find(id);
            if (classschedule == null)
            {
                return HttpNotFound();
            }
            return View(classschedule);
        }

        // POST: classschedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "classID,classlevel,classday,classtimings")] classschedule classschedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classschedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classschedule);
        }

        // GET: classschedules/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            classschedule classschedule = db.classschedules.Find(id);
            if (classschedule == null)
            {
                return HttpNotFound();
            }
            return View(classschedule);
        }

        // POST: classschedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            classschedule classschedule = db.classschedules.Find(id);
            db.classschedules.Remove(classschedule);
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
