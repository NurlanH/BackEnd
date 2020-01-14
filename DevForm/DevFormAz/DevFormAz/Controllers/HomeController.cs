﻿using DevFormAz.Helper;
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
using System.Web.Helpers;

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
        public ActionResult FormPage(Form form, string tagname, List<HttpPostedFileBase> formImg)
        {
            if (ModelState.IsValid)
            {
                if (form != null)
                {
                    var tagArr = tagname.Split(',');
                    Form newForm = new Form();
                    newForm.Name = form.Name;
                    newForm.FormTime = DateTime.Now;
                    newForm.Description = form.Description;
                    newForm.UserDetailId = (int)Session["UserId"];
                    db.Forms.Add(newForm);
                    db.SaveChanges();


                    // For tag list
                    for (int i = 0; i < tagArr.Length; i++)
                    {
                        if (tagArr[i] != null && tagArr[i]!="" && tagArr[i] != " ")
                        {
                            TagList tag = new TagList();
                            tag.TagName = tagArr[i];
                            tag.FormId = newForm.Id;
                            db.TagLists.Add(tag);
                        }
                    }


                    // For form image
                    foreach (var img in formImg)
                    {
                        if (img != null)
                        {
                            if (İmageControl.CheckImageSize(img, 4))
                            {

                                var formImages = İmageControl.SaveImage(Server.MapPath("~/Public/Images/PostImgs"), img);
                                FormImage fImg = new FormImage()
                                {
                                    FormId = newForm.Id,
                                    ImageName = formImages
                                };
                                db.FormImages.Add(fImg);
                            };

                        }
                    }
                    db.SaveChanges();

                }
            }
          
            return RedirectToAction("FormPage", "Home");
        }


        //View Post
        public ActionResult FormView(int? id)
        {
            if (id != null)
            {
                FormViewPageVM frmVm = new FormViewPageVM()
                {
                    Form = db.Forms.Find(id),
                    TagLists = db.TagLists.Where(fId => fId.FormId == id).ToList(),
                    FormImages = db.FormImages.Where(imgId => imgId.FormId == id).ToList()
                };
                
                if ((int?)Session["UserId"] != null)
                {
                    var userId = (int)Session["UserId"];


                    var checkUserView = db.FormViewCounts.Any(u => u.UserId == userId && u.FormId == id);
                    if (!checkUserView)
                    {
                        FormViewCount Vcount = new FormViewCount()
                        {
                            FormId = (int)id,
                            UserId = (int)Session["UserId"]

                        };
                        db.FormViewCounts.Add(Vcount);
                        db.SaveChanges();
                    }
                }
                return View(frmVm);
            }
            else
            {
                return RedirectToAction("FormPage", "Home");
            }

        }



        //Edit user Form
        public ActionResult EditForm(int? id)
        {
            if(id != null)
            {
                var checkUser = (int?)Session["UserId"];

                if (db.Forms.Find(id).UserDetailId == checkUser)
                {
                    return View(db.Forms.Find(id));
                }
                else
                {
                    return RedirectToAction("FormPage", "Home");
                }
            }
            else
            {
                return RedirectToAction("FormPage", "Home");
            }
          
        }

        public async Task<int> AddLike(int id)
        {
            
                Form form = await db.Forms.FirstOrDefaultAsync(c => c.Id == id);
                int userId = (int)Session["UserId"];
                var userIsLiked = db.FormLikes.Any(x => x.FormId == form.Id && x.UserId == userId);
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
            if (removelike != null)
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
            int userId = (int)Session["UserId"];
            UserViewModel vm = new UserViewModel()
            {
                GetUserDetail = db.UserDetails.Find(userId),
                Forms = db.Forms.Where(u => u.UserDetailId == userId).ToList(),
                Tags = db.TagLists.ToList()
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
        public ActionResult UserPanel([Bind(Exclude = "Image")]UserDetail userChanges, string firstname, string lastname, string email,string newPassword,string rePassword, HttpPostedFileBase image, string skills, string checker)
        {

            var user = db.UserDetails.Find((int)Session["UserId"]);

            //For user IMG
            if (image != null && İmageControl.CheckImageType(image))
            {
                if (İmageControl.CheckImageSize(image, 8))
                {
                    if (user.Image != null)
                    {
                        İmageControl.DeleteImage(Server.MapPath("~/Public/Images/UsersFolder/ProfilePic"), user.Image);
                    }

                    var imgName = İmageControl.SaveImage(Server.MapPath("~/Public/Images/UsersFolder/ProfilePic"), image);
                    user.Image = imgName;
                    Session["UserImage"] = user.Image;
                }
            }


            //For user Skills
            if(skills != null)
            {
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
            }

            

            

            //For new password
            if(newPassword != "" && newPassword != null )
            {
                var checkNewPass = newPassword.Length;

                if (checkNewPass >= 6 && newPassword == rePassword)
                {
                    user.User.Password = Crypto.HashPassword(newPassword);
                }
            }
           
            
            //For change email
            if (user.User.Email != email)
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

            user.User.FirstName = firstname;
            user.User.Lastname = lastname;
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