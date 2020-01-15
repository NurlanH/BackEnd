using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class Subscribe
    {
        public int Id { get; set; }
        public int FollowId { get; set; }
        public int FollowerId { get; set; }
    }
}