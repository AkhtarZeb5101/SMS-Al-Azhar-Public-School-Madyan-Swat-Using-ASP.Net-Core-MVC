using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;

namespace SchoolManagementSystem.Controllers
{
    public class HomeWorkTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: HomeWorkTables
        public ActionResult Index()
        {
            var homeWorkTables = db.HomeWorkTables.Include(h => h.ClassTable).Include(h => h.HomeWorkTypeTable).Include(h => h.StudentTable);
            return View(homeWorkTables.ToList());
        }

        // GET: HomeWorkTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWorkTable homeWorkTable = db.HomeWorkTables.Find(id);
            if (homeWorkTable == null)
            {
                return HttpNotFound();
            }
            return View(homeWorkTable);
        }

        // GET: HomeWorkTables/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name",0);
            ViewBag.HomeWorkTypeID = new SelectList(db.HomeWorkTypeTables, "HomeWorkTypeID", "HomeWorkTypeName",0);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", 0);
            return View(new HomeWorkTable());
        }

        // POST: HomeWorkTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HomeWorkID,HomeWorkTypeID,ClassID,StudentID,HomeWorkTitle,HomeWorkDescription,SubmitDate,DocPath")] HomeWorkTable homeWorkTable)
        {
            homeWorkTable.SubmitDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.HomeWorkTables.Add(homeWorkTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", homeWorkTable.ClassID);
            ViewBag.HomeWorkTypeID = new SelectList(db.HomeWorkTypeTables, "HomeWorkTypeID", "HomeWorkTypeName", homeWorkTable.HomeWorkTypeID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", homeWorkTable.StudentID);
            return View(homeWorkTable);
        }

        // GET: HomeWorkTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWorkTable homeWorkTable = db.HomeWorkTables.Find(id);
            if (homeWorkTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", homeWorkTable.ClassID);
            ViewBag.HomeWorkTypeID = new SelectList(db.HomeWorkTypeTables, "HomeWorkTypeID", "HomeWorkTypeName", homeWorkTable.HomeWorkTypeID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", homeWorkTable.StudentID);
            return View(homeWorkTable);
        }

        // POST: HomeWorkTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HomeWorkID,HomeWorkTypeID,ClassID,StudentID,HomeWorkTitle,HomeWorkDescription,SubmitDate,DocPath")] HomeWorkTable homeWorkTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeWorkTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", homeWorkTable.ClassID);
            ViewBag.HomeWorkTypeID = new SelectList(db.HomeWorkTypeTables, "HomeWorkTypeID", "HomeWorkTypeName", homeWorkTable.HomeWorkTypeID);
            ViewBag.StudentID = new SelectList(db.StudentTables, "StudentID", "Name", homeWorkTable.StudentID);
            return View(homeWorkTable);
        }

        // GET: HomeWorkTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWorkTable homeWorkTable = db.HomeWorkTables.Find(id);
            if (homeWorkTable == null)
            {
                return HttpNotFound();
            }
            return View(homeWorkTable);
        }

        // POST: HomeWorkTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeWorkTable homeWorkTable = db.HomeWorkTables.Find(id);
            db.HomeWorkTables.Remove(homeWorkTable);
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
