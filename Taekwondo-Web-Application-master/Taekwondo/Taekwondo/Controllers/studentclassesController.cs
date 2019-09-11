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
    public class studentclassesController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: studentclasses
        public ActionResult Index(string studentID, string startdate = null)
        {
          var studentclasses = db.studentclasses.Include(s => s.classschedule).Include(s => s.student);

            if (!string.IsNullOrEmpty(studentID))
            {
                studentclasses = studentclasses.Where(x => x.studentID == studentID);
                return View(studentclasses.ToList());
            }

            if (startdate != null)
            {
                //this will default to current date if for whatever reason the date supplied by user did not parse successfully
                studentclasses = db.studentclasses.Where(x => x.attendeddate.Equals(startdate));
                return View(studentclasses.ToList());
            }
            return View(studentclasses.ToList());
        }

        // GET: studentclasses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentclass studentclass = db.studentclasses.Find(id);
            if (studentclass == null)
            {
                return HttpNotFound();
            }
            return View(studentclass);
        }

        // GET: studentclasses/Create
        public ActionResult Create()
        {
            ViewBag.classID = new SelectList(db.classschedules, "classID", "classlevel");
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID");
            return View();
        }

        // POST: studentclasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stdclassID,studentID,classID,attendeddate")] studentclass studentclass)
        {
            if (ModelState.IsValid)
            {
                db.studentclasses.Add(studentclass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.classID = new SelectList(db.classschedules, "classID", "classlevel", studentclass.classID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID", studentclass.studentID);
            return View(studentclass);
        }

        // GET: studentclasses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentclass studentclass = db.studentclasses.Find(id);
            if (studentclass == null)
            {
                return HttpNotFound();
            }
            ViewBag.classID = new SelectList(db.classschedules, "classID", "classlevel", studentclass.classID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID", studentclass.studentID);
            return View(studentclass);
        }

        // POST: studentclasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stdclassID,studentID,classID,attendeddate")] studentclass studentclass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentclass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.classID = new SelectList(db.classschedules, "classID", "classlevel", studentclass.classID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID", studentclass.studentID);
            return View(studentclass);
        }

        // GET: studentclasses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentclass studentclass = db.studentclasses.Find(id);
            if (studentclass == null)
            {
                return HttpNotFound();
            }
            return View(studentclass);
        }

        // POST: studentclasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            studentclass studentclass = db.studentclasses.Find(id);
            db.studentclasses.Remove(studentclass);
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
