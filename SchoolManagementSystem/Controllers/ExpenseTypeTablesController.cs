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
    public class ExpenseTypeTablesController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        // GET: ExpenseTypeTables
        public ActionResult Index()
        {
            return View(db.ExpenseTypeTables.ToList());
        }

        // GET: ExpenseTypeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseTypeTable expenseTypeTable = db.ExpenseTypeTables.Find(id);
            if (expenseTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(expenseTypeTable);
        }

        // GET: ExpenseTypeTables/Create
        public ActionResult Create()
        {
            return View(new ExpenseTypeTable());
        }

        // POST: ExpenseTypeTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpensesTypeID,Name,IsActive")] ExpenseTypeTable expenseTypeTable)
        {
            if (ModelState.IsValid)
            {
                db.ExpenseTypeTables.Add(expenseTypeTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenseTypeTable);
        }

        // GET: ExpenseTypeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseTypeTable expenseTypeTable = db.ExpenseTypeTables.Find(id);
            if (expenseTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(expenseTypeTable);
        }

        // POST: ExpenseTypeTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpensesTypeID,Name,IsActive")] ExpenseTypeTable expenseTypeTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenseTypeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseTypeTable);
        }

        // GET: ExpenseTypeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseTypeTable expenseTypeTable = db.ExpenseTypeTables.Find(id);
            if (expenseTypeTable == null)
            {
                return HttpNotFound();
            }
            return View(expenseTypeTable);
        }

        // POST: ExpenseTypeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpenseTypeTable expenseTypeTable = db.ExpenseTypeTables.Find(id);
            db.ExpenseTypeTables.Remove(expenseTypeTable);
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
