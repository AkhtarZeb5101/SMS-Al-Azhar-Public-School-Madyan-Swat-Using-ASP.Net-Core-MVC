using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class FeeReportsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: FeeReports
        public ActionResult CustomTution()
        {
            var alltutionfee = db.SubmissionFeeTables.Where(e => e.SubmissionDate >= DateTime.Now && e.SubmissionDate <= DateTime.Now).ToList().OrderByDescending(e => e.SubmissionFeeID);
            return View(alltutionfee);
        }

        [HttpPost]
        public ActionResult CustomTution(DateTime fromDate, DateTime toDate)
        {
            var alltutionfee = db.SubmissionFeeTables.Where(e => e.SubmissionDate >= DateTime.Now && e.SubmissionDate <= DateTime.Now).ToList().OrderByDescending(e => e.SubmissionFeeID);
            return View(alltutionfee);
        }


        public ActionResult CustomAnnual()
        {
            var allannulfee = db.StudentPromotTables.Where(e => e.PromoteDate >= DateTime.Now && e.PromoteDate <= DateTime.Now && e.IsSubmit == true).ToList().OrderByDescending(e => e.StudentPromotID);
            return View(allannulfee);
        }

        [HttpPost]
        public ActionResult CustomAnnual(DateTime fromDate, DateTime toDate)
        {
            var allannulfee = db.StudentPromotTables.Where(e => e.PromoteDate >= fromDate && e.PromoteDate <= toDate && e.IsSubmit == true).ToList().OrderByDescending(e => e.StudentPromotID);
            return View(allannulfee);
        }
    }
}