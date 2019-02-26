using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentSystem;

namespace StudentSystem.Controllers
{
    public class MarkdetailsController : Controller
    {
        private StudentInfoEntities db = new StudentInfoEntities();

        // GET: Markdetails
        public ActionResult Index()
        {
            var markdetails = db.Markdetails.Include(m => m.ClassDetail).Include(m => m.Student);
            return View(markdetails.ToList());
        }

        // GET: Markdetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markdetail markdetail = db.Markdetails.Find(id);
            if (markdetail == null)
            {
                return HttpNotFound();
            }
            return View(markdetail);
        }

        // GET: Markdetails/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.ClassDetails, "ID", "ClassName");
            ViewBag.Roll = new SelectList(db.Students, "ID", "FirstName");
            return View();
        }

        // POST: Markdetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Roll,ClassID,ObtainMark,OutOf,Percentage,Pass")] Markdetail markdetail)
        {
            if (ModelState.IsValid)
            {
                db.Markdetails.Add(markdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassDetails, "ID", "ClassName", markdetail.ClassID);
            ViewBag.Roll = new SelectList(db.Students, "ID", "FirstName", markdetail.Roll);
            return View(markdetail);
        }

        // GET: Markdetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markdetail markdetail = db.Markdetails.Find(id);
            if (markdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassDetails, "ID", "ClassName", markdetail.ClassID);
            ViewBag.Roll = new SelectList(db.Students, "ID", "FirstName", markdetail.Roll);
            return View(markdetail);
        }

        // POST: Markdetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Roll,ClassID,ObtainMark,OutOf,Percentage,Pass")] Markdetail markdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassDetails, "ID", "ClassName", markdetail.ClassID);
            ViewBag.Roll = new SelectList(db.Students, "ID", "FirstName", markdetail.Roll);
            return View(markdetail);
        }

        // GET: Markdetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Markdetail markdetail = db.Markdetails.Find(id);
            if (markdetail == null)
            {
                return HttpNotFound();
            }
            return View(markdetail);
        }

        // POST: Markdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Markdetail markdetail = db.Markdetails.Find(id);
            db.Markdetails.Remove(markdetail);
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
