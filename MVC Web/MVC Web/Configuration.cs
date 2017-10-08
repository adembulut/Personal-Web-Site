using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVC_Web.Models;
namespace MVC_Web
{
    public class Configuration : CreateDatabaseIfNotExists<dkPersonelWebContext>
    {
        protected override void Seed(dkPersonelWebContext context)
        {
            User user = new User
            {

                Username = "adembulut",
                Password = "merdiye",
                NameSurname = "Adem Bulut",
                Gsm = "+90 507 727 80 14",
                Address = "Gültepe OFFICE B Blok Kat:9 No:902",
                isAdmin = true,
                Mail = "adembulutapp@gmail.com",
                WebSite = "adem-bulut.com"
            };
            context.Users.Add(user);
            Category cat = new Category { Name = "General" };
            context.Categories.Add(cat);
            context.SaveChanges();

            Article newArticle = new Article{

                AddedDate = DateTime.Now,
                CategoryID=cat.Id,
                UserID = user.Id,
                Header="First Article",
                Content="Content of First Article",
                ImagePath="http://placehold.it/600x400"
            }
            context.Articles.Add(newArticle);
            context.SaveChanges();
            Comment firstComment = new Comment{
                NameSurname = "Tom Cruise",
                WebSite="http://example.com",
                ArticleID=newArticle.Id,
                Text = "Right Content",
                AddedDate = DateTime.Now
            };
            context.SaveChanges();
            Work work = new Work{
                AddedDate=DateTime.Now,
                Content="First Work",
                Header="First Work"
                ,ImagePath="http://placehold.it/600x400",
                UserID=user.Id
           };
            context.SaveChanges();
                
        }
    }
}