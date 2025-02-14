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
    public class StudentTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StudentTables
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var studentTables = db.StudentTables.Include(s => s.ClassTable).Include(s => s.ProgrameTable).Include(s => s.SessionTable).Include(s => s.UserTable).OrderByDescending(s=>s.StudentID);
            return View(studentTables.ToList());
        }


        // GET: StudentTables/Details/5
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
            StudentTable studentTable = db.StudentTables.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            return View(studentTable);
        }

        // GET: StudentTables/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name");
            ViewBag.ProgrameID = new SelectList(db.ProgrameTables, "ProgrameID", "Name");
            ViewBag.SessionID = new SelectList(db.SessionTables, "SessionID", "Name");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            return View(new StudentTable());
        }

        // POST: StudentTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentTable studentTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            studentTable.UserID = 6;
            studentTable.Photo = "/Content/EmployeePhoto/default.png";
            if (ModelState.IsValid)
            {

                var user = new UserTable();
                user.Address = studentTable.Address;
                user.ContactNo = studentTable.ContactNo;
                user.EmailAddress = studentTable.EmailAddress;
                user.FullName = studentTable.Name;
                user.UserName = studentTable.Name;
                user.UserTypeID = 4;
                user.Password = "8888";
                db.UserTables.Add(user);
                db.StudentTables.Add(studentTable);
                studentTable.UserID = user.UserID;
                db.SaveChanges();
                if (studentTable.PhotoFile != null)
                {
                    var folder = "/Content/StudentPhoto";
                    var file = string.Format("{0}.png", studentTable.StudentID);
                    var response = FileHelper.UploadFile.UploadPhoto(studentTable.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        studentTable.Photo = pic;
                        db.Entry(studentTable).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentTable.ClassID);
            ViewBag.ProgrameID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", studentTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTables, "SessionID", "Name", studentTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", studentTable.UserID);
            return View(studentTable);
        }

        // GET: StudentTables/Edit/5
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
            StudentTable studentTable = db.StudentTables.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentTable.ClassID);
            ViewBag.ProgrameID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", studentTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTables, "SessionID", "Name", studentTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", studentTable.UserID);
            return View(studentTable);
        }

        // POST: StudentTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentTable studentTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            studentTable.UserID = userid;
            studentTable.Photo = "/Content/EmployeePhoto/default.png";
            if (ModelState.IsValid)
            {
                if (studentTable.PhotoFile != null)
                {
                    var folder = "/Content/EmployeePhoto";
                    var file = string.Format("{0}.png", studentTable.StudentID);
                    var response = FileHelper.UploadFile.UploadPhoto(studentTable.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        studentTable.Photo = pic;
                    }
                }

                db.Entry(studentTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTables, "ClassID", "Name", studentTable.ClassID);
            ViewBag.ProgrameID = new SelectList(db.ProgrameTables, "ProgrameID", "Name", studentTable.ProgrameID);
            ViewBag.SessionID = new SelectList(db.SessionTables, "SessionID", "Name", studentTable.SessionID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", studentTable.UserID);
            return View(studentTable);
        }

        // GET: StudentTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentTable studentTable = db.StudentTables.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            return View(studentTable);
        }

        // POST: StudentTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentTable studentTable = db.StudentTables.Find(id);
            db.StudentTables.Remove(studentTable);
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
