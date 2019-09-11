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
    public class studentranksController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: studentranks
        public ActionResult Index(String studentID)
        {
            var studentranks = db.studentranks.Include(s => s.rank).Include(s => s.student);
            if (!string.IsNullOrEmpty(studentID))
            {
                studentranks = studentranks.Where(x => x.studentID == studentID);
            }
            return View(studentranks.ToList());
        }

        // GET: studentranks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentrank studentrank = db.studentranks.Find(id);
            if (studentrank == null)
            {
                return HttpNotFound();
            }
            return View(studentrank);
        }

        // GET: studentranks/Create
        public ActionResult Create()
        {
            ViewBag.rankID = new SelectList(db.ranks, "rankID", "rankname");
            ViewBag.studentID = new SelectList(db.students, "studentID", "fname");
            return View();
        }

        // POST: studentranks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stdrankID,studentID,rankID,dateachieved")] studentrank studentrank)
        {
            if (ModelState.IsValid)
            {
                db.studentranks.Add(studentrank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rankID = new SelectList(db.ranks, "rankID", "rankname", studentrank.rankID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "fname", studentrank.studentID);
            return View(studentrank);
        }

        // GET: studentranks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentrank studentrank = db.studentranks.Find(id);
            if (studentrank == null)
            {
                return HttpNotFound();
            }
            ViewBag.rankID = new SelectList(db.ranks, "rankID", "rankname", studentrank.rankID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "fname", studentrank.studentID);
            return View(studentrank);
        }

        // POST: studentranks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stdrankID,studentID,rankID,dateachieved")] studentrank studentrank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentrank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rankID = new SelectList(db.ranks, "rankID", "rankname", studentrank.rankID);
            ViewBag.studentID = new SelectList(db.students, "studentID", "fname", studentrank.studentID);
            return View(studentrank);
        }

        // GET: studentranks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentrank studentrank = db.studentranks.Find(id);
            if (studentrank == null)
            {
                return HttpNotFound();
            }
            return View(studentrank);
        }

        // POST: studentranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            studentrank studentrank = db.studentranks.Find(id);
            db.studentranks.Remove(studentrank);
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
