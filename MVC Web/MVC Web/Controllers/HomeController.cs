using MVC_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Created by Adem-bulut

namespace MVC_Web.Controllers
{
    public class HomeController : Controller
    {
        dkPersonelWebContext db = new dkPersonelWebContext();

        // GET: Home
        public ActionResult Index()
        {
            List<User> Kullanicilar = db.Users.ToList();
            User user = db.Users.FirstOrDefault(x => x.Username=="adembulut");
            
            return View(user);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Contact(Message message)
        {
            if (message != null)
            {
                message.AddedDate = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
                ViewBag.Message = "Your message has been sent!";
                return View();
            }
            ViewBag.Message = "";
            return View();
        }


    }
}