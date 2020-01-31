using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class AdminAccessControl
    {
        public ICollection<User> Users { get; set; }
        public ICollection<Form> Forms { get; set; }
        public ICollection<TagList> TagLists { get; set; }
    }
}