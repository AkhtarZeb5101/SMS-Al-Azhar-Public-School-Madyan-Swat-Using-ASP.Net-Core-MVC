using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private SchoolMgtDbEntities db = new SchoolMgtDbEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string email, string password)
        {
            try
            {


                if (email != null && password != null)
                {
                    var finduser = db.UserTables.Where(u => u.EmailAddress == email && u.Password == password).ToList();
                    if (finduser.Count() == 1)
                    {
    
                        Session["UserID"] = finduser[0].UserID;
                        Session["UserTypeID"] = finduser[0].UserTypeID;
                        Session["FullName"] = finduser[0].FullName;
                        Session["UserName"] = finduser[0].UserName;
                        Session["Password"] = finduser[0].Password;
                        Session["ContactNo"] = finduser[0].ContactNo;
                        Session["EmailAddress"] = finduser[0].EmailAddress;
                        Session["Address"] = finduser[0].Address;
                        var userid = finduser[0].UserID;
                        var studentphoto = db.StudentTables.Where(s => s.UserID == userid).FirstOrDefault();
                        if (studentphoto != null)
                        {
                            Session["Photo"] = studentphoto.Photo;
                        }
                        else
                        {
                            var employee = db.StaffTables.Where(e=>e.UserID == userid).FirstOrDefault();
                            Session["Photo"] = employee.Photo;
                        }
                        //UserTypeID UserTypeName
                            //1   Admin
                            //2   Operator
                            //3   Teacher
                            //4   Student

                        string url = string.Empty;
                        if (finduser[0].UserTypeID == 2)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 3)
                        {

                            return RedirectToAction("About");
                            
                        }

                        else if (finduser[0].UserTypeID == 4)
                        {
                            return RedirectToAction("About");
                        }
                        else if (finduser[0].UserTypeID == 1)
                        {
                            url = "About";

                        }
                        else
                        {
                            url = "About";
                        }

                        return RedirectToAction(url);
                    }
                    else
                    {
                        Session["UserID"] = string.Empty;
                        Session["UserTypeID"] = string.Empty;
                        Session["FullName"] = string.Empty;
                        Session["UserName"] = string.Empty;
                        Session["Password"] = string.Empty;
                        Session["ContactNo"] = string.Empty;
                        Session["EmailAddress"] = string.Empty;
                        Session["Address"] = string.Empty;
                        ViewBag.message = "User Name and Password is incorrect!";

                    }
                }
                else
                {
                    Session["UserID"] = string.Empty;
                    Session["UserTypeID"] = string.Empty;
                    Session["FullName"] = string.Empty;
                    Session["UserName"] = string.Empty;
                    Session["Password"] = string.Empty;
                    Session["ContactNo"] = string.Empty;
                    Session["EmailAddress"] = string.Empty;
                    Session["Address"] = string.Empty;
                    ViewBag.message = "Some unexpected issue is occure please try again!";


                }
            }
            catch (Exception ex)
            {
                Session["UserID"] = string.Empty;
                Session["UserTypeID"] = string.Empty;
                Session["FullName"] = string.Empty;
                Session["UserName"] = string.Empty;
                Session["Password"] = string.Empty;
                Session["ContactNo"] = string.Empty;
                Session["EmailAddress"] = string.Empty;
                Session["Address"] = string.Empty;
                ViewBag.message = "Some unexpected issue is occure please try again!";


            }
            return View("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to Al-Azhar Public School";

            return View();
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var password = db.UserTables.Where(s => s.EmailAddress == email).FirstOrDefault();
            if (password != null)
            {
                WebMail.Send(password.EmailAddress,
              "Password",
              password.Password,

              null,
              null
              , null
              , true
              , null
              , null
              , null
              , null
              , null
              , "azaib5101@gmail.com");
                ViewBag.Message = "Please Check Email!";
            }
            else {
                ViewBag.Message = "Email Not Found!";
            }
            return View();
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ChangePasswordU(string oldpassword, string newpassword, string confirmpassword)
        {
            if (newpassword != confirmpassword)
            {
                ViewBag.Message = "Not Matched!";
                return View("ChangePassword");
            }
            int userid = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            var getuser = db.UserTables.Find(userid);
            if (getuser.Password == oldpassword.Trim())
            {
                getuser.Password = newpassword.Trim();
            }
            else
            {
                ViewBag.Message = "Old Password is Incorrect!";
                return View("ChangePassword");
            }
            db.Entry(getuser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Logout");
        }



        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            Session["FullName"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["Password"] = string.Empty;
            Session["ContactNo"] = string.Empty;
            Session["EmailAddress"] = string.Empty;
            Session["Address"] = string.Empty;
            return RedirectToAction("Login");
        }
    }
}