using Antelope.Data.Models;
using Antelope.Data.Repositories;
using Antelope.Web.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Antelope.Web.Controllers
{
    public class HomeController : Controller
    {
        private AccountRepository _accRepo;
        // GET: Home
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            _accRepo = new AccountRepository(new MainModel());
            return View(_accRepo.GetAll());
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model, string returnUrl)
        {
            // Lets first check if the Model is valid or not
            if (ModelState.IsValid)
            {

                MainModel entities = new MainModel();
                string username = model.Name;
                string password = model.Password;

                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly

                bool userValid = entities.Users.Any(user => user.Name == username && user.Password == password);

                // User found in the database
                if (userValid)
                {

                    FormsAuthentication.SetAuthCookie(username, false);
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
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Filter()
        {
            _accRepo = new AccountRepository(new MainModel());
            return View("Index", _accRepo.GetMatchedAcc(true,true,true));
        }

        [HttpPost]
        public ActionResult Filter(string[] c1, string[] c2, string[] c3)
        {
            _accRepo = new AccountRepository(new MainModel());
            return View("Index", _accRepo.GetMatchedAcc(c1 == null ? false : true, c2 == null ? false : true, c3 == null ? false : true));
        }
        /// <summary>
        /// Chat Action
        /// </summary>
        /// <returns></returns>
        public ActionResult Chat()
        {
            return View();
        }

    }
}