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
    public class ClassDetailsController : Controller
    {
        private StudentInfoEntities db = new StudentInfoEntities();

        // GET: ClassDetails
        public ActionResult Index()
        {
            var classDetails = db.ClassDetails.Include(c => c.Branch);
            return View(classDetails.ToList());
        }

        // GET: ClassDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassDetail classDetail = db.ClassDetails.Find(id);
            if (classDetail == null)
            {
                return HttpNotFound();
            }
            return View(classDetail);
        }

        // GET: ClassDetails/Create
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Trade");
            return View();
        }

        // POST: ClassDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BranchID,ClassName")] ClassDetail classDetail)
        {
            if (ModelState.IsValid)
            {
                db.ClassDetails.Add(classDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Trade", classDetail.BranchID);
            return View(classDetail);
        }

        // GET: ClassDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassDetail classDetail = db.ClassDetails.Find(id);
            if (classDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Trade", classDetail.BranchID);
            return View(classDetail);
        }

        // POST: ClassDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BranchID,ClassName")] ClassDetail classDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Trade", classDetail.BranchID);
            return View(classDetail);
        }

        // GET: ClassDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassDetail classDetail = db.ClassDetails.Find(id);
            if (classDetail == null)
            {
                return HttpNotFound();
            }
            return View(classDetail);
        }

        // POST: ClassDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassDetail classDetail = db.ClassDetails.Find(id);
            db.ClassDetails.Remove(classDetail);
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
