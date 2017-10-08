using System;
using System.Collections.Generic;

namespace MVC_Web.Models
{
    public partial class Comment
    {
        public Comment()
        {
            this.Comment1 = new List<Comment>();
        }

        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string WebSite { get; set; }
        public string Text { get; set; }
        public Nullable<int> ArticleID { get; set; }
        public Nullable<int> CommentID { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public virtual Article Article { get; set; }
        public virtual ICollection<Comment> Comment1 { get; set; }
        public virtual Comment Comment2 { get; set; }
    }
}
