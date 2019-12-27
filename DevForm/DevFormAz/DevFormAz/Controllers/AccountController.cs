using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFormAz.Controllers
{
    public class AccountController : Controller
    {
        // Login Page
        public ActionResult Login()
        {
            return View();
        }

        // Register Page
        public ActionResult Register()
        {
            return View();
        }


        // Forgot Page
        public ActionResult Forgot()
        {
            return View();
        }
    }
}