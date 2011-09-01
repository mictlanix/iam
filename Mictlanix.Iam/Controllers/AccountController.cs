using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Mictlanix.Iam.Models;

namespace Mictlanix.Iam.Controllers
{
    public class AccountController : Controller
    {
        SSContext db = new SSContext();

        bool ValidateUser(string username, string password)
        {
            User user = db.Users.SingleOrDefault(u => u.UserName == username);
            return user != null && user.Password == SHA1(password);
        }

        bool CreateUser(string username, string password, string firstName, string lastName, string email)
        {
            User user = new User
            {
                UserName = username,
                Password = SHA1(password),
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            if (db.Users.SingleOrDefault(u => u.UserName == username) != null)
            {
                throw new Exception(Mictlanix.Iam.Properties.Resources.Message_UserNameAlreadyExists);
            }

            db.Users.Add(user);
            
            return db.SaveChanges() != 0;
        }

        bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            User user = db.Users.SingleOrDefault(u => u.UserName == username);
            string pwd = SHA1(oldPassword);

            if (user == null || user.Password != pwd)
                return false;

            user.Password = SHA1(newPassword);

            return db.SaveChanges() != 0;
        }


        protected string SHA1(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes("" + text);
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            return BitConverter.ToString(sha1.ComputeHash(bytes)).Replace("-", "");
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", Mictlanix.Iam.Properties.Resources.Message_InvalidUserPassword);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user

                try
                {
                    if (CreateUser(model.UserName, model.Password, model.FirstName, model.LastName, model.Email))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", Mictlanix.Iam.Properties.Resources.Message_UnknownError);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);                    
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;

                try
                {
                    changePasswordSucceeded = ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", Mictlanix.Iam.Properties.Resources.Message_ChangePasswordWrong);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
