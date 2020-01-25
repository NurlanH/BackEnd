using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime WritedTime { get; set; }
        public int FormId { get; set; }
        public int UserDetailId { get; set; }
        public virtual Form Form { get; set; }
        public virtual UserDetail UserDetail { get; set; }
    }
}