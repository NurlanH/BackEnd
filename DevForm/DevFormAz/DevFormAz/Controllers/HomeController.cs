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
                    newForm.WhenIsDeleted = DateTime.Now;
                    newForm.FormTime = DateTime.Now;
                    newForm.Description = form.Description;
                    newForm.UserDetailId = (int)Session["UserId"];
                    db.Forms.Add(newForm);
                    db.SaveChanges();


                    // For tag list
                    for (int i = 0; i < tagArr.Length; i++)
                    {
                        if (tagArr[i] != null && tagArr[i] != "" && tagArr[i] != " ")
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
                var findForm = db.Forms.Find(id);
                if ( findForm != null && findForm.isDeleted != true)
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
            else
            {
                return RedirectToAction("FormPage", "Home");
            }

        }



        //Edit user Form
        public ActionResult EditForm(int? id)
        {
            if (id != null)
            {
                var checkUser = (int?)Session["UserId"];

                if (db.Forms.Find(id).UserDetailId == checkUser)
                {
                    FormAndTags ftVm = new FormAndTags()
                    {
                        Form = db.Forms.Find(id),
                        TagLists = db.TagLists.Where(t => t.FormId == id).ToList()
                    };
                    return View(ftVm);
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

        //Edit user Form post
        [HttpPost]
        public ActionResult EditForm(Form form, List<HttpPostedFileBase> addImg, string deleteImg, int? id, string existTag, string editTag)
        {
            var userId = (int?)Session["UserId"];
            if (userId != null && id != null)
            {
                var myForm = db.Forms.Where(f => f.Id == id && f.UserDetailId == userId).SingleOrDefault();
                var delImg = deleteImg.Split(',');

                var oldTag = existTag.Split(' ').ToList(); // Bununla evvelki Taglari saxlayiriq
                var newTag = editTag.Split(' ');

                var checkFormTag = db.TagLists.Where(u => u.FormId == myForm.Id).Select(n => n.TagName).ToList(); // bununla dbda userin daxil edilen bacariginin olub olmamasin yoxlayiriq

                if (ModelState.IsValid && myForm.isDeleted == false)
                {

                    myForm.Name = form.Name;
                    myForm.Description = form.Description;

                    for (int i = 0; i < newTag.Length; i++)
                    {

                        //Add tag
                        if (newTag[i] != "")
                        {
                            if (!checkFormTag.Contains(newTag[i]) && newTag[i] != " " && newTag[i] != null)
                            {
                                TagList tags = new TagList()
                                {
                                    TagName = newTag[i],
                                    FormId = myForm.Id
                                };
                                db.TagLists.Add(tags);
                            }
                            else
                            {
                                oldTag.Remove(newTag[i]);
                            }
                        }



                    }


                    foreach (var item in oldTag)
                    {
                        var deletedTag = db.TagLists.Where(n => n.FormId == myForm.Id && n.TagName == item).FirstOrDefault();
                        if (deletedTag != null)
                        {
                            db.TagLists.Remove(deletedTag);
                        }
                    }




                    foreach (var item in addImg)
                    {
                        if (item != null)
                        {
                            if (İmageControl.CheckImageSize(item, 4))
                            {
                                var image = İmageControl.SaveImage(Server.MapPath("~/Public/Images/PostImgs"), item);
                                FormImage fImg = new FormImage()
                                {
                                    FormId = myForm.Id,
                                    ImageName = image
                                };
                                db.FormImages.Add(fImg);
                            }

                        };

                    };


                    foreach (var item in delImg)
                    {
                        if (item != null && item != "")
                        {
                            db.FormImages.Remove(myForm.FormImages.Where(fd => fd.ImageName == item).SingleOrDefault());
                            İmageControl.DeleteImage(Server.MapPath("~/Public/Images/PostImgs"), item);
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("FormView", "Home", new { myForm.Id });
                }
                else
                {
                    return RedirectToAction("EditForm", "Home", new { myForm.Id });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }

        //DeleteForm
        public ActionResult DeleteForm(int? id)
        {
            var userId = (int?)Session["UserId"];
            if (id != null)
            {
                if (userId != null)
                {
                    var checkForm = db.Forms.Find(id);
                    if (checkForm.UserDetailId == userId)
                    {
                        checkForm.isDeleted = true;
                        checkForm.WhenIsDeleted = DateTime.Now;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("FormPage", "Home");
        }


        //Add like
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


        //Remove like
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
            DevUsersVm vm = new DevUsersVm()
            {
                UserDetails = db.UserDetails.ToList(),
                Subscribes = db.Subscribes.ToList()
            };
            return View(vm);
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
        public ActionResult UserPanel([Bind(Exclude = "Image")]UserDetail userChanges, string firstname, string lastname, string email, string newPassword, string rePassword, HttpPostedFileBase image, string skills, string checker)
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
            if (skills != null)
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
            if (newPassword != "" && newPassword != null)
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