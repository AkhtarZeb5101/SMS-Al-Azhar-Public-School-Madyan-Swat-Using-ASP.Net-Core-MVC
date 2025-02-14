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
    public class StaffTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: StaffTables
        public ActionResult Index()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var staffTables = db.StaffTables.Include(s => s.DesignationTable).Include(s => s.UserTable);
            return View(staffTables.ToList());
        }

        // GET: StaffTables/Details/5
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
            StaffTable staffTable = db.StaffTables.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            return View(staffTable);
        }

        // GET: StaffTables/Create
        public ActionResult Create()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.DesignationID = new SelectList(db.DesignationTables, "DesignationID", "Title");
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName");
            return View(new StaffTable());
        }

        // POST: StaffTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StaffTable staffTable)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            staffTable.Photo = "/Content/EmployeePhoto/default.png";
            staffTable.UserID = 4;
            if (ModelState.IsValid)
            {
                var user = new UserTable();
                user.Address = staffTable.Address;
                user.ContactNo = staffTable.ContactNo;
                user.EmailAddress = staffTable.EmailAddress;
                user.FullName = staffTable.Name;
                user.UserName = staffTable.Name;
                user.UserTypeID = 6;
                user.Password = "8888";
                db.UserTables.Add(user);
                staffTable.UserID = user.UserID;
                db.StaffTables.Add(staffTable);
                db.SaveChanges();

                if (staffTable.PhotoFile != null)
                {
                    var folder = "/Content/EmployeePhoto";
                    var file = string.Format("{0}.png", staffTable.StaffID);
                    var response = FileHelper.UploadFile.UploadPhoto(staffTable.PhotoFile, folder, file);
                    if (response)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        staffTable.Photo = pic;
                        db.Entry(staffTable).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.DesignationID = new SelectList(db.DesignationTables, "DesignationID", "Title", staffTable.DesignationID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", staffTable.UserID);
            return View(staffTable);
        }

        // GET: StaffTables/Edit/5
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
            StaffTable staffTable = db.StaffTables.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationID = new SelectList(db.DesignationTables, "DesignationID", "Title", staffTable.DesignationID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", staffTable.UserID);
            return View(staffTable);
        }

        // POST: StaffTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StaffTable staffTable)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            staffTable.UserID = userid;
            if (ModelState.IsValid)
            {
                
                var folder = "/Content/EmployeePhoto";
                var file = string.Format("{0}.png", staffTable.StaffID);
                var response = FileHelper.UploadFile.UploadPhoto(staffTable.PhotoFile, folder, file);
                if (response)
                {
                    var pic = string.Format("{0}/{1}", folder, file);
                    staffTable.Photo = pic;
                }

                db.Entry(staffTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationID = new SelectList(db.DesignationTables, "DesignationID", "Title", staffTable.DesignationID);
            ViewBag.UserID = new SelectList(db.UserTables, "UserID", "FullName", staffTable.UserID);
            return View(staffTable);
        }

        // GET: StaffTables/Delete/5
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
            StaffTable staffTable = db.StaffTables.Find(id);
            if (staffTable == null)
            {
                return HttpNotFound();
            }
            return View(staffTable);
        }

        // POST: StaffTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            StaffTable staffTable = db.StaffTables.Find(id);
            db.StaffTables.Remove(staffTable);
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
