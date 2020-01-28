using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DevFormAz.Models;
using DevFormAz.DevFormData;
using System.Web.Mvc;
using System.Data.Entity;
using DevFormAz.Extentions;
using System.Net;
using System.Net.Mail;

namespace DevFormAz.Helper
{

    public class FormMethods
    {
        DevFormAzDataBase db = new DevFormAzDataBase();


        //Edit Form tags
        public void EditFrmTag(int formId, string newTag, string oldTag)
        {
            var newTags = (newTag.Split(' ')).ToList(); //new tags
            var oldTags = (oldTag.Split(' ')).ToList(); //old skills;

            var formTags = db.TagLists.Where(f => f.FormId == formId).Select(us => us.TagName).ToList(); //form db tags

            foreach (var tag in newTags)
            {
                if (formTags.Contains(tag) || tag == "" || tag == " ")
                {
                    oldTags.Remove(tag);
                    continue;
                }

                TagList frmTag = new TagList() { TagName = tag, FormId = formId };
                db.TagLists.Add(frmTag);
            }

            foreach (var item in oldTags)
            {
                if (item != "" && item != " ")
                {
                    var deleteTag = db.TagLists.Where(n => n.TagName == item && n.FormId == formId).FirstOrDefault();
                    db.TagLists.Remove(deleteTag);
                }

            }

            db.SaveChanges();
        }


        //Delete form
        public void DeleteForm(int frmId, int userId)
        {
            var checkForm = db.Forms.Find(frmId);
            if (checkForm.UserDetailId == userId)
            {
                checkForm.isDeleted = true;
                checkForm.WhenIsDeleted = DateTime.Now;
                db.SaveChanges();
            }
        }

        // Add Like 
        public async Task Like(int formId, int userId)
        {
            FormLike like = new FormLike()
            {
                FormId = formId,
                UserId = userId
            };
            db.FormLikes.Add(like);
            await db.SaveChangesAsync();
        }

        // Remove Like 
        public async Task Disslike(int formId, int userId)
        {
            var likedUser = await db.FormLikes.Where(x => x.UserId == userId && x.FormId == formId).SingleOrDefaultAsync();
            db.FormLikes.Remove(likedUser);
            await db.SaveChangesAsync();
        }



        //Follow
        public async Task Follow(int followerId, int followId)
        {
            Subscribe sb = new Subscribe()
            {
                FollowerId = followerId,
                FollowId = followId
            };

            db.Subscribes.Add(sb);
            await db.SaveChangesAsync();
        }

        //Unfollow
        public async Task UnFollow(int followerId, int followId)
        {
            var unfollow = await db.Subscribes.Where(unf => unf.FollowerId == followerId && unf.FollowId == followId).SingleOrDefaultAsync();
            db.Subscribes.Remove(unfollow);
            await db.SaveChangesAsync();
        }

        public async Task SendNotificationAsync(List<int> followers , User user)
        {
            Task.Run(() =>
            {
                foreach (var item in followers)
                {
                    var userEmail = db.Users.Find(item).Email;

                    NetworkCredential cred = new NetworkCredential("developerformaz@gmail.com", "Nurlan1998###");
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                    {
                        UseDefaultCredentials = false,
                        Credentials = cred,
                        EnableSsl = true
                    };
                    MailMessage msg = new MailMessage()
                    {
                        Body = $"<p>{user.FirstName + " " + user.Lastname } yeni post paylashdi</p>",
                        Subject = "DevForm Bildirim",
                        IsBodyHtml = true
                    };

                    MailAddress address = new MailAddress(userEmail);
                    msg.To.Add(address);
                    msg.From = new MailAddress("developerformaz@gmail.com");
                    client.Send(msg);
                }
            });
        }

    }

}