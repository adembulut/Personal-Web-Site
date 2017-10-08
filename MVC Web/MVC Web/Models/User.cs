using System;
using System.Collections.Generic;

namespace MVC_Web.Models
{
    public partial class User
    {
        public User()
        {
            this.Articles = new List<Article>();
            this.Works = new List<Work>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<bool> isAdmin { get; set; }
        public string Gsm { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
        public string WebSite { get; set; }
        public string Title { get; set; }
        public string NameSurname { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}
