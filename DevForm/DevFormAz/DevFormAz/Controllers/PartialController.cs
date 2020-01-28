using DevFormAz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevFormAz.DevFormData;

namespace DevFormAz.Controllers
{
    public class PartialController : Controller
    {
        DevFormAzDataBase db = new DevFormAzDataBase();
        // GET: Partial

        public ActionResult SendComment(int? formId,string cmt)
        {
            var userId = (int?)Session["UserId"];

            if(userId != null)
            {
                Comment comment = new Comment()
                {
                    FormId = (int)formId,
                    Description = cmt,
                    UserDetail = db.UserDetails.Find(userId),
                    UserDetailId = (int)userId,
                    WritedTime = DateTime.Now
                };
                db.Comments.Add(comment);
                db.SaveChanges();

                return PartialView("~/Views/Shared/Partials/SendComment.cshtml",comment);
            }
            return RedirectToAction("Login","Account");
        }

    }
}