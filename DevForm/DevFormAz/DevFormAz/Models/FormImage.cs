using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class FormImage
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int FormId { get; set; }
        public virtual Form Form { get; set; }
    }
}