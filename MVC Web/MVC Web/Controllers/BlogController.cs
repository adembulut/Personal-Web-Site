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

        [HttpPost]
        public string AddComment(Comment cmm)
        {
            if (cmm != null)
            {
                if (cmm.CommentID < 1)
                {
                    cmm.CommentID = null;
                }
                cmm.AddedDate = DateTime.Now;
                cmm.isCheck = false;
                db.Comments.Add(cmm);
                db.SaveChanges();
                return "Your comment has been successfully added.";
            }
            else
            {
                return "It was a mistake to post comment.";
            }
        }


    }
}