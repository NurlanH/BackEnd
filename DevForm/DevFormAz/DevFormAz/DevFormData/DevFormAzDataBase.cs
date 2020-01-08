using System;
using System.Collections.Generic;
using System.Data.Entity;
using DevFormAz.Models;
using System.Linq;
using System.Web;

namespace DevFormAz.DevFormData
{
    public class DevFormAzDataBase :DbContext
    {
        public DevFormAzDataBase():base("DevFormAz")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<TagList> TagLists { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<FormLike> FormLikes { get; set; }
        public DbSet<FormDisslike> FormDisslikes { get; set; }

    }
}