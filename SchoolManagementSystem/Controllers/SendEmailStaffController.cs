using DatabaseAccess;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class SendEmailStaffController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();
        // GET: Email
        public ActionResult Index()
        {
            return View(new SendEmail());
        }
        [HttpPost]
        public ActionResult Index(SendEmail email)
        {
            if (ModelState.IsValid)
            {
                var emp = db.StaffTables.ToList();
                foreach (var item in emp)
                {
                    WebMail.Send(item.EmailAddress,
                   email.Name,
                   email.Message,

                   null,
                   null
                   , null
                   , true
                   , null
                   , null
                   , null
                   , null
                   , null
                   , email.Emails);
                }


                return RedirectToAction("Mailsent");
            }
            return View();
        }
        public ActionResult Mailsent()
        {
            return View();
        }
    }
}