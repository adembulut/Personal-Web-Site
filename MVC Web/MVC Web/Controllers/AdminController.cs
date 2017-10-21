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
                    try
                    {
                        string resimyolu = Request.MapPath(ar.ImagePath);
                        if (System.IO.File.Exists(resimyolu))
                            System.IO.File.Delete(resimyolu);
                    }
                    catch (Exception) { }
                    db.Articles.Remove(ar);
                    db.SaveChanges();
                    return RedirectToAction("Blogs");
                }
                return View(ar);
            }
            return RedirectToAction("Blogs");

        }

        public ActionResult Categories()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();

            }
            return View(db.Categories.ToList());
        }

        [HttpPost]
        public ActionResult CategoryAdd(Category category)
        {
            if (category != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Categories", db.Categories.ToList());
            }
            return View();
        }
        public ActionResult CategoryRemove(int? id)
        {
            if (id != null && id > 0)
            {
                Category categor = db.Categories.Find(id);
                if (categor != null && categor.Articles.ToList().Count == 0)
                {
                    db.Categories.Remove(categor);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Categories", db.Categories.ToList());
        }

        [HttpPost]
        public ActionResult CategoryEdit(string EditId, string EditName)
        {
            if (!String.IsNullOrEmpty(EditId) && !String.IsNullOrEmpty(EditName))
            {
                try
                {
                    int Id = Convert.ToInt32(EditId);
                    if (Id > 0 && EditName.Trim().Length > 0)
                    {
                        Category category = db.Categories.Find(Id);
                        if (category != null)
                        {
                            category.Name = EditName;
                            db.SaveChanges();
                            TempData["Message"] = "Category edited.";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Name must consist of at least 1 character";
                    }
                }
                catch (Exception)
                {
                    TempData["Message"] = "An Error corrupted.";
                }

            }
            return RedirectToAction("Categories", db.Categories.ToList());

        }

        public ActionResult Comments()
        {
            return View(db.Comments.OrderBy(x => x.isCheck).ToList());
        }

        public ActionResult CommentOk(int? Id)
        {
            if (Id != null && Id > 0)
            {
                Comment cm = db.Comments.Find(Id);
                if (cm != null)
                {
                    cm.isCheck = true;
                    db.SaveChanges();

                }
            }
            return RedirectToAction("Comments", db.Comments.OrderBy(x => x.isCheck).ToList());
        }

        [HttpPost]
        public string CommentDelete(string Id)
        {
            try
            {
                int id = Convert.ToInt32(Id);
                Comment cm = db.Comments.Find(id);
                if (cm != null)
                {
                    db.Comments.Remove(cm);
                    db.SaveChanges();
                    return "Comment Deleted!";
                }
            }
            catch (Exception) { return "An Error Corrupted!"; }
            return "An Error Corrupted";
        }

        public ActionResult Works()
        {
            return View(db.Works.OrderBy(x => x.AddedDate).ToList());
        }

        public ActionResult WorkAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WorkAdd(Work work, HttpPostedFileBase foto)
        {
            if (work != null && foto != null)
            {
                work.AddedDate = DateTime.Now;
                work.UserID = (Session["Admin"] as User).Id;

                string path = Server.MapPath("~/Content/uploads/");
                Directory.CreateDirectory(path);
                WebImage img = new WebImage(foto.InputStream);
                img.Resize(600, 480, false, false);
                FileInfo fotoinfo = new FileInfo(foto.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Save("~/Content/uploads/" + newfoto);
                string imagePath = "~/Content/uploads/" + newfoto;
                work.ImagePath = imagePath;
                db.Works.Add(work);
                db.SaveChanges();
                return RedirectToAction("Works", db.Works.OrderBy(x => x.AddedDate).ToList());


            }
            return View();
        }

        [HttpPost]
        public string WorkDel(int? id)
        {
            if (id != null && id > 0)
            {
                Work w = db.Works.Find(id);
                if (w != null)
                {
                    try
                    {
                        string imagePath = "";
                        try
                        {
                            imagePath = Request.MapPath(w.ImagePath);
                        }
                        catch { }
                        
                        if (!String.IsNullOrEmpty(imagePath)&& System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        db.Works.Remove(w);
                        db.SaveChanges();

                        return "Work is Deleted!";
                    }
                    catch (Exception) { }


                }
            }
            return "An Error Corrupted!";
        }

        public ActionResult WorkDetail(int? id)
        {
            if (id != null && id > 0)
            {
                Work w = db.Works.Find(id);
                if (w != null)
                {
                    return View(w);
                }
            }
            return RedirectToAction("Works", db.Works.OrderBy(x => x.AddedDate).ToList());
        }

        public ActionResult WorkEdit(int? id)
        {
            if (id != null && id > 0)
            {
                Work w = db.Works.Find(id);
                if (w != null)
                {
                    return View(w);
                }
            }
            return RedirectToAction("Work", db.Works.OrderBy(x => x.AddedDate).ToList());
        }

        [HttpPost]
        public ActionResult WorkEdit(Work w,HttpPostedFileBase foto)
        {
            if (w != null)
            {
                Work work = db.Works.Find(w.Id);
                work.Header = w.Header;
                work.Content = w.Content;

                if (foto != null)
                {
                    WebImage image = new WebImage(foto.InputStream);
                    image.Resize(600, 400, false, false);
                    string klasor = "~/Content/uploads/";
                    string uzanti = (new FileInfo(foto.FileName)).Extension;
                    Directory.CreateDirectory(Request.MapPath(klasor));
                    string onek = Guid.NewGuid().ToString();
                    string yeniyol = klasor + "/" + onek + uzanti;
                    image.Save(yeniyol);
                    string eskiresim = Request.MapPath(work.ImagePath);
                    work.ImagePath = yeniyol;
                    if (System.IO.File.Exists(eskiresim))
                        System.IO.File.Delete(eskiresim);
                    
                }
                db.SaveChanges();
                return View(work);
            }
           return RedirectToAction("Works", db.Works.OrderBy(x => x.AddedDate).ToList());
        }

       
    }
}