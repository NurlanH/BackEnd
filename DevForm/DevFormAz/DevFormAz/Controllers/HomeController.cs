using DevFormAz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFormAz.Controllers
{
    public class HomeController : Controller
    {

        //Home page
        public ActionResult Index()
        {
            return View();
        }


        //Form page
        public ActionResult FormPage()
        {
            return View();
        }


        //Tag page
        public ActionResult TagPage()
        {
            return View();
        }


        //Users page
        public ActionResult UsersInspectPage()
        {
            return View();
        }

        //UserPanel page
        [DevAuth]
        public ActionResult UserPanel()
        {
            return View();
        }

    }
}