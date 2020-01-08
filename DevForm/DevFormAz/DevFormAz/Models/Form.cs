using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int UserDetailId { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual ICollection<TagList> TagLists { get; set; }

        public virtual ICollection<FormLike> FormLikes { get; set; }
        public virtual ICollection<FormDisslike> FormDisslikes { get; set; }

    }
}