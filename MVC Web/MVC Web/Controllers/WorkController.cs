using MVC_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Web.Controllers
{
    public class WorkController : Controller
    {
        dkPersonelWebContext db = new dkPersonelWebContext();
        // GET: Work
        public ActionResult Index()
        {
            List<Work> myworks = db.Works.OrderByDescending(x => x.AddedDate).ToList();
            return View(myworks);
        }
    }
}