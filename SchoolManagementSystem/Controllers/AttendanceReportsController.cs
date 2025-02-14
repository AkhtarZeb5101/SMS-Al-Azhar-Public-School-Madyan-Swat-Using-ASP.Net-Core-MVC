using DatabaseAccess;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class AttendanceReportsController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: AttendanceReports
        public ActionResult StudentAttedance(int? id)
        {

            if (id == 0)
            {
                int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
                id = db.StudentTables.Where(e => e.UserID == userid).FirstOrDefault().StudentID;
            }

            var classid = db.StudentPromotTables.Where(p => p.StudentID == id && p.IsActive == true).FirstOrDefault().ClassID;
            var studentattandance = db.AttendanceTables.Where(a => a.StudentID == id && a.ClassID == classid).OrderByDescending(a=>a.AttendanceID);
            return View(studentattandance);
        }

        public ActionResult AllStudent()
        {
            var studentattandance = db.AttendanceTables.OrderByDescending(a => a.AttendDate);
            return View(studentattandance);
        }

        public ActionResult Staff(int? id)
        {

            if (id == 0)
            {
                int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
                var staff = db.StaffTables.Where(e => e.UserID == userid).FirstOrDefault();
                id = staff != null ? staff.StaffID : 0;
            }
            List<StaffAttendanceReport> staffAttendancelist = new List<StaffAttendanceReport>();
            var staffattendance = db.StaffAttendanceTables.Where(a => a.StaffAttendanceID == id).OrderByDescending(a => a.StaffAttendanceID);
            foreach (var item in staffattendance)
            {
                       var attend = new StaffAttendanceReport();
                attend.Name = item.StaffTable.Name;
                attend.Designation = item.StaffTable.DesignationTable.Title;
                attend.AttendDate = item.AttendDate;
                attend.ComingTime = item.ComingTime;
                attend.ClosingTime = item.ClosingTime;
                attend.DutyHour = ((TimeSpan)item.ClosingTime - (TimeSpan)item.ComingTime);

     

            staffAttendancelist.Add(attend);
            }


            return View(staffAttendancelist);
        }

        public ActionResult AllStaff()
        {

            List<StaffAttendanceReport> staffAttendancelist = new List<StaffAttendanceReport>();
            var staffattendance = db.StaffAttendanceTables.OrderByDescending(a => a.StaffAttendanceID);
            foreach (var item in staffattendance)
            {
                var attend = new StaffAttendanceReport();
                attend.Name = item.StaffTable.Name;
                attend.Designation = item.StaffTable.DesignationTable.Title;
                attend.AttendDate = item.AttendDate;
                attend.ComingTime = item.ComingTime;
                attend.ClosingTime = item.ClosingTime;
                attend.DutyHour = ((TimeSpan)item.ClosingTime - (TimeSpan)item.ComingTime);
                staffAttendancelist.Add(attend);
            }
            return View(staffAttendancelist);
        }
    }
}