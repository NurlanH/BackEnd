using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevFormAz.Models
{
    public class DevUsersVm
    {
        public List<UserDetail> UserDetails { get; set; }
        public List<Subscribe> Subscribes { get; set;  }
    }
}