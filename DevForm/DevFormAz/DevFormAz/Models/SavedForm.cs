using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class SavedForm
    {
        public int Id { get; set; }
        public int UserDetailId { get; set; }
        public int FormId { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual Form Form { get; set; }
    }
}