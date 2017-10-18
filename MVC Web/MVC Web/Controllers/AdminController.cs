using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Web.Models;
using System.Web.Helpers;
using System.IO;
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

        public ActionResult BlogAdd()
        {
            return View(db.Categories.ToList());
        }

        [HttpPost]
        public ActionResult BlogAdd(Article art, HttpPostedFileBase foto)
        {
            try
            {
                string path = Server.MapPath("~/Content/uploads/");
                Directory.CreateDirectory(path);
                WebImage img = new WebImage(foto.InputStream);
                img.Resize(600, 480, false, false);
                FileInfo fotoinfo = new FileInfo(foto.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Save("~/Content/uploads/" + newfoto);
                string imagePath = "~/Content/uploads/" + newfoto;
                Article article = new Article
                {
                    CategoryID = art.CategoryID,
                    Content = art.Content,
                    Header = art.Header,
                    ImagePath = imagePath,
                    AddedDate = DateTime.Now,
                    UserID = (Session["Admin"] as User).Id
                };
                db.Articles.Add(article);
                db.SaveChanges();

                return RedirectToAction("Blogs");
            }
            catch (Exception)
            {
                return View(db.Categories.ToList());
            }

        }

        public ActionResult Blogs()
        {
            return View(db.Articles.ToList());
        }

        public ActionResult BlogEdit(int? id)
        {
            if (id != null && id > 0)
            {
                Article article = db.Articles.Find(id);
                if (article != null)
                    return View(article);
            }
            return RedirectToAction("Blogs");
        }


        [HttpPost]
        public ActionResult BlogEdit(Article article, HttpPostedFileBase foto)
        {

            if (article != null)
            {
                if (article.Id > 0)
                {
                    Article eski = db.Articles.Find(article.Id);
                    if (eski != null)
                    {
                        eski.Header = article.Header;
                        eski.Content = article.Content;
                        if (foto != null)
                        {
                            WebImage image = new WebImage(foto.InputStream);
                            image.Resize(600, 400, false, false);

                            FileInfo fotoinfo = new FileInfo(foto.FileName);
                            string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                            image.Save("~/Content/uploads/" + newfoto);
                            string imagePath = "~/Content/uploads/" + newfoto;


                            string eskiResim = eski.ImagePath;
                            eski.ImagePath = imagePath;
                            try
                            {
                                eskiResim = Request.MapPath(eskiResim);
                            }
                            catch (Exception) { }

                            if (!String.IsNullOrEmpty(eskiResim))
                            {
                                if (System.IO.File.Exists(eskiResim))
                                {
                                    System.IO.File.Delete(eskiResim);
                                }
                            }
                            db.SaveChanges();
                        }
                        else
                        {
                            db.SaveChanges();
                            return RedirectToAction("Blogs");
                        }

                    }
                }
            }
            return RedirectToAction("Blogs");
        }

        public ActionResult BlogDetail(int? id)
        {
            if (id != null && id > 0)
            {
                Article article = db.Articles.Find(id);
                if (article != null)
                {
                    return View(article);
                }
            }
            return RedirectToAction("Blogs");
        }

        public ActionResult BlogRemove(int? id)
        {
            if (id != null && id > 0)
            {
                Article article = db.Articles.Find(id);
                if (article != null)
                {
                    return View(article);
                }
            }
            return RedirectToAction("Blogs");
        }

        [HttpPost]
        public ActionResult BlogRemove(Article article)
        {
            if (article != null)
            {
                Article ar = db.Articles.Find(article.Id);
                if (ar != null)
                {
                    List<Comment> liste = ar.Comments.ToList();
                    db.Comments.RemoveRange(liste);
                    db.Articles.Remove(ar);
                    db.SaveChanges();
                    return RedirectToAction("/Admin/Blogs");
                }
                return View(ar);
            }
            return RedirectToAction("Blogs");
        
        }
    }
}