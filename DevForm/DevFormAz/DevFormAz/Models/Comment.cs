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
        public DateTime WritedTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public int LikeCount { get; set; }
        public int DisslikeCount { get; set; }

        public int FormId { get; set; }
        public int UserId { get; set; }
        public virtual Form Form { get; set; }
        public virtual User User { get; set; }
    }
}