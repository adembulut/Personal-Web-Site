using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Web.Models;
namespace MVC_Web.Controllers
{
    public class BlogController : Controller
    {
        dkPersonelWebContext db = new dkPersonelWebContext();

        // GET: Blog
        public ActionResult Index()
        {
            List<Article> articles = db.Articles.OrderByDescending(x => x.AddedDate).ToList();
            return View(articles);
        }

        public ActionResult Detail(int? id)
        {
            if (id != null && id > 0)
            {
                Article article = db.Articles.Find(id);
                if (article != null)
                {
                    return View(article);
                }
            }
            return RedirectToAction("Index");
        }
    }
}