using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Web.Models;
namespace MVC_Web.Controllers
{
    public class AdminController : BaseController
    {
        dkPersonelWebContext db = new dkPersonelWebContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["Admin"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AdminEdit()
        {
            User user = Session["Admin"] as User;
            return View(user);
        }
        [HttpPost]
        public ActionResult AdminEdit(User user)
        {
            User asil = Session["Admin"] as User;
            User eski = db.Users.Find(asil.Id);
            if (eski != null)
            {
                eski.NameSurname = user.NameSurname;
                eski.Gsm = user.Gsm;
                eski.Mail = user.Mail;
                eski.Username = user.Username;
                eski.Password = user.Password;
                eski.Address = user.Address;
                eski.Title = user.Title;
                eski.WebSite = user.WebSite;
                db.SaveChanges();
                ViewBag.result = "Save is completed.";
                return View(eski);
                
            }
            ViewBag.result = "An error occurred.";
            return View(eski);
        }
    }
}