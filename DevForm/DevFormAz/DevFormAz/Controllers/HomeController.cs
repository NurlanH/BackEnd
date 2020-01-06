using DevFormAz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevFormAz.Models;
using DevFormAz.DevFormData;
using DevFormAz.Extentions;

namespace DevFormAz.Controllers
{
    public class HomeController : Controller
    {
        DevFormAzDataBase db = new DevFormAzDataBase();

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


        //Profile page
        [DevAuth]
        public ActionResult ProfilePage()
        {
            UserViewModel vm = new UserViewModel()
            {
                //Forms = new List<Form>(),
                GetUserDetail = db.UserDetails.Find((int)Session["UserId"])
            };
            return View(vm);
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
            var userID = (int)Session["UserId"];
            var userDetail = db.UserDetails.Find(userID);
            return View(userDetail);
        }

        [HttpPost]
        public ActionResult UserPanel([Bind(Exclude="Image")]UserDetail userChanges,string firstname,string lastname,string email,HttpPostedFileBase image,string skills)
        {
            var user = db.UserDetails.Find((int)Session["UserId"]);
            if (image != null && İmageControl.CheckImageType(image))
            {
                if (İmageControl.CheckImageSize(image, 8))
                {
                    var imgName = İmageControl.SaveImage(Server.MapPath("~/Public/Images/UsersFolder/ProfilePic"), image);
                    user.Image = imgName;
                }
            }
            
            if(skills != null && skills != "")
            {
                var skillarr = skills.Split(' ');



                for (var i = 0; i<skillarr.Length;i++)
                {
                    if (skillarr[i] != " " && db.Skills.Find((int)Session["UserId"]).Name != skillarr[i])
                    db.Skills.Add(new Skill() { Name = skillarr[i],UserDetailId = user.Id });
                }
                
            }
            user.User.FirstName = firstname;
            user.User.Lastname = lastname;
            user.User.Email = email;
            
            user.Biography = userChanges.Biography;
            user.Country = userChanges.Country;
            user.GithubLink = userChanges.GithubLink;
            user.Specialty = userChanges.Specialty;
            user.LinkedinLink = userChanges.LinkedinLink;

            db.SaveChanges();
            return RedirectToAction("ProfilePage","Home");
        }


    }
}