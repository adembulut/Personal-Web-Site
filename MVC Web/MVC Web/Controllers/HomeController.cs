﻿using MVC_Web.Models;
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
            User user = db.Users.FirstOrDefault(x => x.isAdmin == true);
            return View(user);
        }
    }
}