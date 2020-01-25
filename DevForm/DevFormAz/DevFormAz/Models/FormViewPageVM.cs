using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class FormViewPageVM
    {
        public Form Form { get; set; }
        public ICollection<TagList> TagLists { get; set; }
        public ICollection<FormImage> FormImages { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}