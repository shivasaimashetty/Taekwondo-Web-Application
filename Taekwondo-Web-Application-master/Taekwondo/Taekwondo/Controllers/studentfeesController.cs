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
    public class studentfeesController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: studentfees
        public ActionResult Index(string startdate, string enddate)
        {
            DateTime d1, d2;

            d1 = Convert.ToDateTime(startdate);
            d2 = Convert.ToDateTime(enddate);

            if (startdate != null && enddate != null)
            {
                //this will default to current date if for whatever reason the date supplied by user did not parse successfully
                var studentfees = db.studentfees.Where(x => x.paiddate >= d1 && x.paiddate <= d2);
                return View(studentfees.ToList());
            }
            else
            {
                var studentfees = db.studentfees.Include(s => s.fee).Include(s => s.student);
                return View(studentfees.ToList());
            }
            //return View();

        }

        // GET: studentfees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentfee studentfee = db.studentfees.Find(id);
            if (studentfee == null)
            {
                return HttpNotFound();
            }
            return View(studentfee);
        }

        // GET: studentfees/Create
        public ActionResult Create()
        {
            ViewBag.feeID = new SelectList(db.fees, "feeID", "feedesc");
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID");
            return View();
        }

        // POST: studentfees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stdfeeID,studentID,feeID,paiddate")] studentfee studentfee)
        {
            if (ModelState.IsValid)
            {
                db.studentfees.Add(studentfee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.feeID = new SelectList(db.fees, "feeID", "feedesc", studentfee.feeID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID", studentfee.studentID);
            return View(studentfee);
        }

        // GET: studentfees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentfee studentfee = db.studentfees.Find(id);
            if (studentfee == null)
            {
                return HttpNotFound();
            }
            ViewBag.feeID = new SelectList(db.fees, "feeID", "feedesc", studentfee.feeID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID", studentfee.studentID);
            return View(studentfee);
        }

        // POST: studentfees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stdfeeID,studentID,feeID,paiddate")] studentfee studentfee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentfee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.feeID = new SelectList(db.fees, "feeID", "feedesc", studentfee.feeID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "studentID", studentfee.studentID);
            return View(studentfee);
        }

        // GET: studentfees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentfee studentfee = db.studentfees.Find(id);
            if (studentfee == null)
            {
                return HttpNotFound();
            }
            return View(studentfee);
        }

        // POST: studentfees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            studentfee studentfee = db.studentfees.Find(id);
            db.studentfees.Remove(studentfee);
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
