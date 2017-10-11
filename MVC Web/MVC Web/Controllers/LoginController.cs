using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Web.Models;
namespace MVC_Web.Controllers
{
    public class LoginController : Controller
    {
        // Created by Adem Bulut
        dkPersonelWebContext db = new dkPersonelWebContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (user != null)
            {
                if (user.Username.Length > 0 && user.Password.Length > 0)
                {
                    User admin = db.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
                    if (admin != null)
                    {
                        Session["admin"] = admin;
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }


            return View();

        }
    }
}