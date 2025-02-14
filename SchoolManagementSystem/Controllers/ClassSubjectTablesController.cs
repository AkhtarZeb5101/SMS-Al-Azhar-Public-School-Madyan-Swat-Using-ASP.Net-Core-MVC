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
    public class ClassSubjectTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ClassSubjectTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var classSubjectTables = db.ClassSubjectTables.Include(c => c.ClassTable).Include(c => c.SubjectTable);
            return View(classSubjectTables.ToList());
        }

        // GET: ClassSubjectTables/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables.Where(c=>c.IsActive == true), "ClassID", "Name");
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name");
            return View(new ClassSubjectTable());
        }

        // POST: ClassSubjectTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClassSubjectTable classSubjectTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

          
            if (ModelState.IsValid)
            {
                var classname = db.ClassTables.Where(c => c.ClassID == classSubjectTable.ClassID).SingleOrDefault();
                if (classname != null)
                {
                    if (!classSubjectTable.Name.Contains(classname.Name))
                    {
                        classSubjectTable.Name = classSubjectTable.Name + "-" + classname.Name;
                    }
                }
                db.ClassSubjectTables.Add(classSubjectTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables.Where(c => c.IsActive == true), "ClassID", "Name", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables.Where(c => c.IsActive == true), "ClassID", "Name", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // POST: ClassSubjectTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClassSubjectTable classSubjectTable)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            
            if (ModelState.IsValid)
            {
                var classname = db.ClassTables.Where(c => c.ClassID == classSubjectTable.ClassID).SingleOrDefault();
                if (classname != null)
                {
                    if (!classSubjectTable.Name.Contains(classname.Name))
                    {
                        classSubjectTable.Name = classSubjectTable.Name + "-" + classname.Name;
                    }
                }
                db.Entry(classSubjectTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables.Where(c => c.IsActive == true), "ClassID", "Name", classSubjectTable.ClassID);
            ViewBag.SubjectID = new SelectList(db.SubjectTables, "SubjectID", "Name", classSubjectTable.SubjectID);
            return View(classSubjectTable);
        }

        // GET: ClassSubjectTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            if (classSubjectTable == null)
            {
                return HttpNotFound();
            }
            return View(classSubjectTable);
        }

        // POST: ClassSubjectTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ClassSubjectTable classSubjectTable = db.ClassSubjectTables.Find(id);
            db.ClassSubjectTables.Remove(classSubjectTable);
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
