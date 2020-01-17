using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public DateTime InJob { get; set; }
        public DateTime OutJob { get; set; }
        public string JobDesc { get; set; }
        public int UserDetailId { get; set; }
        
        public virtual UserDetail UserDetail { get; set; }
    }
}