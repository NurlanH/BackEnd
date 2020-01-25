using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using DevFormAz.DevFormData;
using DevFormAz.Models;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DevFormAz.Helper
{

    public class UserMethods
    {
        DevFormAzDataBase db = new DevFormAzDataBase();

        //Add & remove skill for user
        public void AddRemoveSkills(UserDetail user, string skills, string checker)
        {
            var newSkills = (skills.Split(' ')).ToList(); //new skills
            var oldSkill = (checker.Split(' ')).ToList(); //old skills;
            var userSkills = user.Skills.Select(us => us.Name).ToList(); //user db skills

            foreach (var skill in newSkills)
            {
                if (userSkills.Contains(skill) || skill == "" || skill == " ")
                {
                    oldSkill.Remove(skill);
                    continue;
                }

                Skill newSkill = new Skill() { Name = skill, UserDetailId = user.Id };
                db.Skills.Add(newSkill);
            }

            foreach (var item in oldSkill)
            {
                if (item != "" && item != " ")
                {
                    var deleteSkill = db.Skills.Where(n => n.Name == item && n.UserDetailId == user.Id).FirstOrDefault();
                    db.Skills.Remove(deleteSkill);
                }

            }

            db.SaveChanges();

        }



        //Change Password
        public void ChangePassword(UserDetail user, string password, string repassword)
        {
            if (password == repassword)
            {
                user.User.Password = Crypto.HashPassword(password);
                db.SaveChanges();
            }
        }



        //Change Email
        public void ChangeEmail(UserDetail user, string newEmail)
        {
            if (!db.Users.Any(u => u.Email == newEmail))
            {
                user.User.Email = newEmail;
            }
        }
    }
}