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
        public DevFormAzDataBase():base("DevFormDb")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
    }
}