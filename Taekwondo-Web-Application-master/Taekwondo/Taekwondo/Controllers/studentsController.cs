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
    public class studentsController : Controller
    {
        private TaekwondoEntities db = new TaekwondoEntities();

        // GET: students
        public ActionResult Index(string city, string status, string year, string rankID)
        {


            var citylist = new List<string>();

            var cityqry = from s in db.students
                          orderby s.city
                          select s.city;

            citylist.AddRange(cityqry.Distinct());

            ViewBag.city = new SelectList(citylist);


            var statlist = new List<string>();

            var statqry = from s in db.students
                          orderby s.status
                          select s.status;

            statlist.AddRange(statqry.Distinct());

            ViewBag.status = new SelectList(statlist);


            var yrlist = new List<string>();

            var yrqry = from s in db.students
                        orderby s.joindate
                        select (s.joindate).Substring(0, 4);

            yrlist.AddRange(yrqry.Distinct());

            ViewBag.year = new SelectList(yrlist);

            ViewBag.rankID = new SelectList(db.ranks, "rankID", "rankbelt");

            var students = from a in db.students.Include(s => s.rank)
                           select a;


            if (!string.IsNullOrEmpty(year))
            {
                students = students.Where(x => (x.joindate.Substring(0, 4)) == year);
            }

            if (!string.IsNullOrEmpty(status))
            {
                students = students.Where(x => x.status == status);
            }

            if (!string.IsNullOrEmpty(city))
            {
                students = students.Where(x => x.city == city);
            }

            if (!string.IsNullOrEmpty(rankID))
            {
                students = students.Where(x => x.rank.rankID == rankID);
            }
            return View(students);
        }


        // GET: students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: students/Create
        public ActionResult Create()
        {
            ViewBag.rankID = new SelectList(db.ranks, "rankID", "rankbelt");
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentID,fname,lname,dob,email,phonenumber,address1,address2,city,province,zipcode,status,rankID,joindate,parentdetails")] student student)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.rankID = new SelectList(db.ranks, "rankID", "rankbelt", student.rankID);
            return View(student);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentID,fname,lname,dob,email,phonenumber,address1,address2,city,province,zipcode,status,rankID,joindate,parentdetails")] student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rankID = new SelectList(db.ranks, "rankID", "rankbelt", student.rankID);
            return View(student);
        }

        // GET: students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            student student = db.students.Find(id);
            student.status = "Inactive";
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
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
