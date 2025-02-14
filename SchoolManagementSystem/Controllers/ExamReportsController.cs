using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class ExamReportsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: ExamReports
        public ActionResult PrintDMC()
        {
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");
            return View(new List<ExamMarksTable>());
        }


       [HttpPost()]
        public ActionResult PrintDMC(int? promoteid, int? examid)
        {
           
            ViewBag.ExamID = new SelectList(db.ExamTables, "ExamID", "Title");
            var promoterecord = db.StudentPromotTables.Find(promoteid);
            if (promoterecord != null)
            {

                var listmarks = db.ExamMarksTables.Where(e => e.ClassSubjectTable.ClassID == promoterecord.ClassID && e.ExamID == examid && e.StudentID == promoterecord.StudentID);
                if (listmarks != null)
                {

                }
                return View(listmarks);
            }
            
            return View(new List<ExamMarksTable>());
        }
    }
}