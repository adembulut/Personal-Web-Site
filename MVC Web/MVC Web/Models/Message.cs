using System;
using System.Collections.Generic;

namespace MVC_Web.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
    }
}
