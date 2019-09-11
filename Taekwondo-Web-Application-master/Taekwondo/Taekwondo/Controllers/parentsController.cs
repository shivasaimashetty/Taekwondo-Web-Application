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
    public class parentsController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: parents
        public ActionResult Index(String studentID)
        {

            var parents = db.parents.Include(p => p.student);
            
            if (!string.IsNullOrEmpty(studentID))
            {
                parents = parents.Where(x => x.studentID == studentID);
            }

            return View(parents.ToList());
        }
    

        // GET: parents/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent parent = db.parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // GET: parents/Create
        public ActionResult Create()
        {
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID");
            return View();
        }

        // POST: parents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "parentID,fathername,fatheremail,fathermobile,mothername,motheremail,mothermobile,emergencycontact,studentID")] parent parent)
        {
            if (ModelState.IsValid)
            {
                db.parents.Add(parent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID", parent.studentID);
            return View(parent);
        }

        // GET: parents/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent parent = db.parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID", parent.studentID);
            return View(parent);
        }

        // POST: parents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "parentID,fathername,fatheremail,fathermobile,mothername,motheremail,mothermobile,emergencycontact,studentID")] parent parent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.studentID = new SelectList(db.students, "studentID", "StudentID", parent.studentID);
            return View(parent);
        }

        // GET: parents/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            parent parent = db.parents.Find(id);
            if (parent == null)
            {
                return HttpNotFound();
            }
            return View(parent);
        }

        // POST: parents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            parent parent = db.parents.Find(id);
            db.parents.Remove(parent);
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
