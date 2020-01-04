using DevFormAz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevFormAz.DevFormData;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;

namespace DevFormAz.Controllers
{
    public class AccountController : Controller
    {
        DevFormAzDataBase db = new DevFormAzDataBase();

        // Login Page
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email,string password)
        {
            if (ModelState.IsValid)
            {
                var reqUser = db.Users.Where(e => e.Email == email).SingleOrDefault();
                if(reqUser != null)
                {
                    var secureUser = Crypto.VerifyHashedPassword(reqUser.Password, password);
                    if (secureUser == true)
                    {
                        if(reqUser.IsActive == true)
                        {
                            Session["UserId"] = reqUser.Id;
                            return RedirectToAction("FormPage", "Home");
                        }
                        else
                        {
                            ViewBag.LoginMsg = "Hesabınız aktiv edilməyib. Zəhmət olmasa mailinizə nəzər yetirin (Qeyd: Əgər DevForm`dan hər hansı bir mail gəlməyibsə zəhmət olmasa spam bölümünə nəzər yetirin)";
                        }
                       
                    }
                    else
                    {
                        ViewBag.LoginMsg = "Şifrə yalnışdır";
                    }
                }
                else
                {
                    ViewBag.LoginMsg = "Sistemdə belə bir istifadəçi tapılmadı";
                }
            }
            else
            {
                ViewBag.LoginMsg = "Bütün xanaları doldurmalısınız";
            }
            return View();
        }

        // Register Page
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user,string rePassword)
        {
            if (ModelState.IsValid)
            {
                var haveUser = db.Users.Any(u => u.Email == user.Email);

                if (!haveUser && user.Password == rePassword)
                {

                    db.Users.Add(user);
                    UserDetail UDetail = new UserDetail();
                    UDetail.UserId = user.Id;
                    db.UserDetails.Add(UDetail);
                    db.SaveChanges();
                    EmailSend(user.UserControlPoint, user.Email);
                    return RedirectToAction("Login", "Account");
                }

                
            }
            return View();
        }

        // Forgot Page
        public ActionResult Forgot()
        {
            return View();
        }


        //Email send
        public ActionResult EmailSend(Guid id,string email)
        {
            try
            {
                NetworkCredential cred = new NetworkCredential("developerformaz@gmail.com", "Nurlan1998###");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = cred;
                client.EnableSsl = true;

                MailMessage msg = new MailMessage();
                msg.Body = $"<a href = 'https://localhost:44309/Account/EmailAccept?token={id}'>Hesabınızı təsdiq edin </a>";
                msg.Subject = "Hesabın aktiv edilməsi";
                msg.IsBodyHtml = true;
                MailAddress address = new MailAddress(email);
                msg.To.Add(address);
                msg.From = new MailAddress("developerformaz@gmail.com");
                client.Send(msg);
                ViewBag.EmailMsg = "Hesabını aktiv etmək üçün sizə mail göndərdik!";
                return View("Login");
            }
            catch
            {
                ViewBag.EmailMsg = "Səhv baş verdi!";
                return View("Login");
            }
                
        }

        public ActionResult EmailAccept()
        {
            var token = Guid.Parse(Request.QueryString["token"]);
            if(token != null)
            {
                var verifyUser = db.Users.Where(i => i.UserControlPoint == token).SingleOrDefault();
                if(verifyUser != null)
                {
                    var userPass = verifyUser.Password;
                    verifyUser.Password = Crypto.HashPassword(verifyUser.Password);
                    verifyUser.UserControlPoint = Guid.NewGuid();
                    verifyUser.IsActive = true;
                    db.SaveChanges();
                    ViewBag.EmailMsg = "Hesabınız uğurla aktiv edildi";
                    Login(verifyUser.Email, userPass);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.EmailMsg = "Əməliyyat zamanı səhv baş verdi. Zəhmət olmasa bir daha cəhd edin";
                    return View("Login");
                }
               
            }
            return View();
        }


        //LogOut
        public ActionResult LogOut()
        {
            
                Session["UserId"] = null;
                return RedirectToAction("Index", "Home");
         
        }
    }
}