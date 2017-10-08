using System;
using System.Collections.Generic;

namespace MVC_Web.Models
{
    public partial class Work
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public virtual User User { get; set; }
    }
}
