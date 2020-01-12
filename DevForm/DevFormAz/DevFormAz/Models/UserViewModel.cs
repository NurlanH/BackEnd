using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class UserViewModel
    {
        public UserDetail GetUserDetail { get; set; }
        public List<Form> Forms { get; set; }
        public List<TagList> Tags { get; set; }
    }
}