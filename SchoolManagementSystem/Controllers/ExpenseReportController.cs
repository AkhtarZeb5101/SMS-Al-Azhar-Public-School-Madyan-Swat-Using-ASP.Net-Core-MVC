using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ExpenseReportController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: ExpenseReport
        public ActionResult AllExpenses()
        {
            var allexpense = db.ExpensesTables.ToList().OrderByDescending(e=>e.ExpensesID);
            return View(allexpense);
        }

        public ActionResult CustomExpenses()
        {
            var allexpense = db.ExpensesTables.Where(e => e.ExpensesDate >= DateTime.Now && e.ExpensesDate <= DateTime.Now).ToList().OrderByDescending(e => e.ExpensesID);
            return View(allexpense);
        }

        [HttpPost]
        public ActionResult CustomExpenses(DateTime fromDate, DateTime toDate)
        {
            var allexpense = db.ExpensesTables.Where(e=>e.ExpensesDate >= fromDate && e.ExpensesDate <= toDate ).ToList().OrderByDescending(e => e.ExpensesID);
            return View(allexpense);
        }
    }
}