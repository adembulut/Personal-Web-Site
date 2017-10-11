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
    }
}