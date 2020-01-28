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
        readonly DevFormAzDataBase db = new DevFormAzDataBase();
        FormMethods helperMethods = new FormMethods();
        UserMethods helperUserMethod = new UserMethods();

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
                Forms = db.Forms.OrderByDescending(i=>i.Id).ToList(),
                TagLists = db.TagLists.ToList()
            };
            return View(formVm);
        }

        [HttpPost]
        public ActionResult FormPage(Form form, string tagname, List<HttpPostedFileBase> formImg)
        {

            var userId = (int)Session["UserId"];
            if (ModelState.IsValid)
            {
                if (form != null)
                {
                    
                    var tagArr = tagname.Split(',');


                    Form newForm = new Form() {
                        Name = form.Name,
                        WhenIsDeleted = DateTime.Now,
                        FormTime = DateTime.Now,
                        Description = form.Description,
                        UserDetailId = (int)Session["UserId"]
                    };
                    db.Forms.Add(newForm);
                    db.SaveChanges();


                    // For tag list
                    for (int i = 0; i < tagArr.Length; i++)
                    {
                        if (tagArr[i] != null && tagArr[i] != "" && tagArr[i] != " ")
                        {
                            TagList tag = new TagList()
                            {
                                TagName = tagArr[i],
                                FormId = newForm.Id
                            };
                           
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

                    var mySubs = db.Subscribes.Where(f => f.FollowId == userId).Select(fo => fo.FollowerId).ToList();
                    var myInfo = db.Users.Find(userId);
                    db.SaveChanges();
                    helperMethods.SendNotificationAsync(mySubs, myInfo);
                    return RedirectToAction("FormPage","Home");
                }
            }
            return View();
        }


        //Forn view page
        public ActionResult FormView(int? id)
        {
            if (id != null)
            {
                var findForm = db.Forms.Find(id);

                if (findForm != null && findForm.isDeleted != true)
                {
                    FormViewPageVM frmVm = new FormViewPageVM()
                    {
                        Form = db.Forms.Find(id),
                        TagLists = db.TagLists.Where(fId => fId.FormId == id).ToList(),
                        FormImages = db.FormImages.Where(imgId => imgId.FormId == id).ToList(),
                        Comments = db.Comments.Where(c => c.FormId == id).OrderByDescending(u=>u.WritedTime).ToList()
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

                if (ModelState.IsValid && myForm.isDeleted == false)
                {

                    myForm.Name = form.Name;
                    myForm.Description = form.Description;

                    //Edit or Add tag
                    helperMethods.EditFrmTag(myForm.Id, editTag, existTag);


                    //Add img form
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

                    //Delete img form
                    foreach (var item in delImg)
                    {
                        if (item != null && item != "")
                        {
                            db.FormImages.Remove(myForm.FormImages.Where(fd => fd.ImageName == item).SingleOrDefault());
                            İmageControl.DeleteImage(Server.MapPath("~/Public/Images/PostImgs"), item);
                        }
                    };

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
            if (id != null && userId != null)
            {
                helperMethods.DeleteForm((int)id, (int)userId);
            }
            return RedirectToAction("FormPage", "Home");
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
                Tags = db.TagLists.ToList(),
                Subscribes = db.Subscribes.ToList()
            };
            return View(vm);
        }


        [HttpGet]
        public ActionResult ProfileViewPage(int? id)
        {
            if (id != null)
            {
                var checkUser = (int?)Session["UserId"];

                if (checkUser != null && id == checkUser)
                {
                    return RedirectToAction(nameof(ProfilePage));
                }
                else
                {
                    UserViewModel vm = new UserViewModel()
                    {
                        GetUserDetail = db.UserDetails.Find(id),
                        Forms = db.Forms.Where(u => u.UserDetailId == id).ToList(),
                        Tags = db.TagLists.ToList(),
                        Subscribes = db.Subscribes.ToList()
                    };
                    return View("ProfilePage", vm);
                }
            }
            else
            {
                return RedirectToAction("UsersInspectPage", "Home");
            }
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
            return View(db.UserDetails.Find(userID));
        }


        [HttpPost]
        public ActionResult UserPanel([Bind(Exclude = "Image")]UserDetail userChanges, string firstname, string lastname, string email, string newPassword, string rePassword, HttpPostedFileBase image, string skills, string checker)
        {

            var user = db.UserDetails.Find((int)Session["UserId"]);

            user.User.FirstName = firstname;
            user.User.Lastname = lastname;
            user.Biography = userChanges.Biography;
            user.Country = userChanges.Country;
            user.GithubLink = userChanges.GithubLink;
            user.Specialty = userChanges.Specialty;
            user.LinkedinLink = userChanges.LinkedinLink;

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
                }
            }


            //For user Skills
            if (skills != null)
            {
                helperUserMethod.AddRemoveSkills(user, skills, checker);
            }


            //For new password
            if (newPassword != null && newPassword.Length >= 6)
            {
                helperUserMethod.ChangePassword(user, newPassword, rePassword);
            }


            //For change email
            if (user.User.Email != email)
            {
                helperUserMethod.ChangeEmail(user, email);
            }

            db.SaveChanges();
            return RedirectToAction("UserPanel", "Home");
        }


        //Add & remove like
        public async Task<int> Like(int id)
        {
            Form form = await db.Forms.FirstOrDefaultAsync(c => c.Id == id);
            int userId = (int)Session["UserId"];
            var userIsLiked = await db.FormLikes.AnyAsync(x => x.FormId == form.Id && x.UserId == userId);

            if (!userIsLiked)
            {
                await helperMethods.Like(form.Id, userId);
            }
            else
            {
                await helperMethods.Disslike(form.Id, userId);
            }
            return form.FormLikes.Count();
        }



        //Subscribe
        public async Task<int> SubscribeUser(int? id)
        {
            var userId = (int?)Session["UserId"];

            if (userId != null && id != null)
            {
                var checkUser = db.Subscribes.Any(u => u.FollowerId == userId && u.FollowId == id);
                if (!checkUser)
                {
                    await helperMethods.Follow((int)userId, (int)id);
                }
                else
                {
                    await helperMethods.UnFollow((int)userId, (int)id);
                }

            }
            return await db.Subscribes.Where(f => f.FollowId == id).CountAsync();
        }

    }
}