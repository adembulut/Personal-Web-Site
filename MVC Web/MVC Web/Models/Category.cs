using System;
using System.Collections.Generic;

namespace MVC_Web.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Articles = new List<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
