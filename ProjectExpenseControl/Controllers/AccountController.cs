using ProjectExpenseControl.CustomAuthentication;
using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjectExpenseControl.Services;

namespace ProjectExpenseControl.Controllers
{
    
    public class AccountController : Controller
    {
        private  AreaRepository _area = new AreaRepository();
        private  RoleRepository _role = new RoleRepository();
        private  UserRepository _user = new UserRepository();

        // GET: Account
        public ActionResult Index()
        {
            CustomSerializeModel user = new CustomSerializeModel();
            user = (CustomSerializeModel)Session["user"];
            if(user != null)
            {
                ViewBag.idUser = user.UserId;
                ViewBag.nameUser = user.FirstName;
                ViewBag.roleUser = user.RoleName[0];
                return View();
            }
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.Email, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.Email, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Roles.Select(r => r.TUSR_DES_TYPE).ToList()
                        };
                        Session["user"] = userModel;
                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, loginView.Email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            ModelState.AddModelError("", "Algo salió mal: El Usuario y la Contraseña son inválidos.");
            return View(loginView);
        }

        [CustomAuthorize(Roles = "Administrador")]
        [HttpGet]
        public ActionResult Registration()
        {
            List<Area> lAreas = _area.GetAll();            
            ViewBag.listaAreas = lAreas;

            return View();
        }

        [CustomAuthorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult Registration(User User)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;
            
            
            if (ModelState.IsValid)
            {
                // Email Verification  
                string email = Membership.GetUserNameByEmail(User.USR_DES_EMAIL);
                if (!string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("Warning Email", "Sorry: Email already Exists");
                    return View(User);
                }

                //Save User Data   
                User.USR_FH_CREATED = DateTime.Now;
                User.USR_FH_LAST_LOGIN = DateTime.Now;
                if (_user.Create(User))
                {
                    messageRegistration = "Su cuenta ha sido creada satisfactoriamente.";
                    statusRegistration = true;

                    ViewBag.Message = messageRegistration;
                    ViewBag.Status = statusRegistration;
                    return View("Registration");
                }

                //Verification Email  
                //VerificationEmail(User.Email, User.ActivationCode.ToString());
                //VerificationEmail(User.Email, user.ActivationCode.ToString());             
            }
            else
            {
                messageRegistration = "Algo salió mal!";
            }

            List<Area> lAreas = _area.GetAll();
            List<Role> lRoles = _role.GetAll();
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;
            ViewBag.listaAreas = lAreas;
            ViewBag.listaRoles = lRoles;

            return View(User);
        }

       
        //[HttpGet]
        //public ActionResult ActivationAccount(string id)
        //{
        //    bool statusAccount = false;
        //    using (AuthenticationDB dbContext = new DataAccess.AuthenticationDB())
        //    {
        //        var userAccount = dbContext.Users.Where(u => u.ActivationCode.ToString().Equals(id)).FirstOrDefault();

        //        if (userAccount != null)
        //        {
        //            userAccount.IsActive = true;
        //            dbContext.SaveChanges();
        //            statusAccount = true;
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Something Wrong !!";
        //        }

        //    }
        //    ViewBag.Status = statusAccount;
        //    return View();
        //}

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            Session["User"] = null;

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            var fromEmail = new MailAddress("divadchl@gmail.com", "Activation Account - AKKA");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "******";
            string subject = "Activation Account !";

            string body = "<br/> Please click on the following link in order to activate your account" + "<br/><a href='" + link + "'> Activation Account ! </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })
                smtp.Send(message);
        }
    }
}