using DevFormAz.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevFormAz.Models;
using DevFormAz.DevFormData;
using DevFormAz.Extentions;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Data.Entity;

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
            FormTagViewModel formVm = new FormTagViewModel()
            {
                Forms = db.Forms.ToList(),
                TagLists = db.TagLists.ToList()
            };
            return View(formVm);
        }

        [HttpPost]
        public ActionResult FormPage(Form form ,string tagname)
        {
            if(form != null)
            {
                var tagArr = tagname.Split(',');
                Form newForm = new Form();
                newForm.Name = form.Name;
                newForm.Description = form.Description;
                newForm.UserDetailId = (int)Session["UserId"];
                db.Forms.Add(newForm);
                db.SaveChanges();
                if (tagArr != null)
                {
                    for (int i = 0; i < tagArr.Length; i++)
                    {
                        TagList tag = new TagList();
                        tag.TagName = tagArr[i];
                        tag.FormId = newForm.Id;
                        tag.MonthlyUseCount = 0;
                        tag.DailyUseCount = 0;
                        db.TagLists.Add(tag);
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("FormPage","Home");
        }

      
        //View Post
        public ActionResult FormView(int id)
        {

            return View(db.Forms.Find(id));
        }

        public async Task<int>  AddLike(int id)
        {
            Form form =await  db.Forms.FirstOrDefaultAsync(c => c.Id == id);
            int userId = (int)Session["UserId"];
            var userIsLiked = db.FormLikes.Any(x=>x.FormId == form.Id && x.UserId == userId);
            if (!userIsLiked)
            {
                FormLike like = new FormLike()
                {
                    FormId = form.Id,
                    UserId = userId
                };
                db.FormLikes.Add(like);
                await db.SaveChangesAsync();
                return form.FormLikes.Count();
            }
            else
            {
                var newCount = RemoveLike(form.Id);
                return await newCount;
            }
        }
       

        public async Task<int> RemoveLike(int id)
        {
            Form form = await db.Forms.FirstOrDefaultAsync(c => c.Id == id);
            var userId = (int)Session["UserId"];
            var removelike = await db.FormLikes.Where(x => x.UserId == userId && x.FormId == form.Id).SingleOrDefaultAsync();
            if(removelike != null)
            {
                db.FormLikes.Remove(removelike);
                await db.SaveChangesAsync();
            }
           
            return form.FormLikes.Count();
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
                GetUserDetail = db.UserDetails.Find((int)Session["UserId"]),
                Forms = db.Forms.ToList()
            };
            Session["UserImage"] = vm.GetUserDetail.Image;
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
                        if(user.Image != null)
                        {
                            İmageControl.DeleteImage(Server.MapPath("~/Public/Images/UsersFolder/ProfilePic"), user.Image);
                        }
                        
                        var imgName = İmageControl.SaveImage(Server.MapPath("~/Public/Images/UsersFolder/ProfilePic"), image);
                        user.Image = imgName;
                        Session["UserImage"] = user.Image;
                     }
                }


                var skillarr = skills.Split(' ');

                var userCustomSkills = checker.Split(' ').ToList(); // Bununla evvelki bacariqlarin saxlayiriq
                var checkUserSkill = user.Skills.Select(u => u.Name).ToArray(); // bununla dbda userin daxil edilen bacariginin olub olmamasin yoxlayiriq

                for (var i = 0; i < skillarr.Length; i++)
                {
                    if (!checkUserSkill.Contains(skillarr[i]) && skillarr[i] != " " && skillarr[i] != "")
                    {
                        db.Skills.Add(new Skill() { Name = skillarr[i].ToUpper(), UserDetailId = user.Id });
                    }
                    else
                    {
                        if (!userCustomSkills.Contains(null))
                            userCustomSkills.Remove(skillarr[i]);
                    }
                }

                foreach (var item in userCustomSkills)
                {
                    var deletedSkill = user.Skills.Where(n => n.Name == item).FirstOrDefault();
                    if (deletedSkill != null)
                    {
                        db.Skills.Remove(deletedSkill);
                    }

                }

                user.User.FirstName = firstname;
                user.User.Lastname = lastname;

            if(user.User.Email != email)
            {
                if (!db.Users.Any(u => u.Email == email))
                {
                    user.User.Email = email;
                }
                else
                {
                    ViewBag.EmailErrorMsg = "Belə bir email artıq movcuddur";
                    return RedirectToAction("UserPanel", "Home");
                }
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