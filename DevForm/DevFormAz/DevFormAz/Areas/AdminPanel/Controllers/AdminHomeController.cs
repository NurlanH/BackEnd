using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DevFormAz.DevFormData;
using DevFormAz.Helper;
using DevFormAz.Models;

namespace DevFormAz.Areas.AdminPanel.Controllers
{
    public class AdminHomeController : Controller
    {
        readonly DevFormAzDataBase db = new DevFormAzDataBase();
        
        [AdminAccess]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user != null && user.Role == "admin")
            {
                var checkManager = db.Users.Where(e => e.Email == user.Email).SingleOrDefault();
                if (Crypto.VerifyHashedPassword(checkManager.Password, user.Password))
                {
                    Session["AdminId"] = checkManager.Id;
                    return RedirectToAction("Index", "AdminHome");
                }
            }
            return View();
        }
    }
}