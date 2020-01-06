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
        public ActionResult UserPanel([Bind(Exclude="Image")]UserDetail userChanges,string firstname,string lastname,string email,HttpPostedFileBase image,string skills,string checker)
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

            
                var skillarr = skills.Split(' ');

                var userCustomSkills = checker.Split(' ').ToList(); // Bununla evvelki bacariqlarin saxlayiriq
                var checkUserSkill = user.Skills.Select(u => u.Name).ToArray(); // bununla dbda userin daxil edilen bacariginin olub olmamasin yoxlayiriq

                for (var i = 0; i<skillarr.Length;i++)
                {
                    if(!checkUserSkill.Contains(skillarr[i]) && skillarr[i] != " " && skillarr[i] != "")
                    {
                        db.Skills.Add(new Skill() { Name = skillarr[i], UserDetailId = user.Id });
                    }
                    else
                    {
                        if(!userCustomSkills.Contains(null))
                        userCustomSkills.Remove(skillarr[i]);
                    }
                }

                foreach(var item in userCustomSkills)
                {
                    var deletedSkill = user.Skills.Where(n => n.Name == item).FirstOrDefault();
                    if (deletedSkill!=null)
                    {
                        db.Skills.Remove(deletedSkill);
                    }
                    
                }
            
            user.User.FirstName = firstname;
            user.User.Lastname = lastname;

            if(!db.Users.Any(u => u.Email == email))
            {
                user.User.Email = email;
            }
            else
            {
                ViewBag.EmailErrorMsg = "Belə bir email artıq movcuddur";
                return RedirectToAction("UserPanel","Home");
            }
            
            
            user.Biography = userChanges.Biography;
            user.Country = userChanges.Country;
            user.GithubLink = userChanges.GithubLink;
            user.Specialty = userChanges.Specialty;
            user.LinkedinLink = userChanges.LinkedinLink;

            db.SaveChanges();
            return RedirectToAction("UserPanel", "Home");
        }


    }
}