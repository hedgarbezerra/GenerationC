using GenerationC.Database.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GenerationC.Controllers
{
    public class LoginController : ConfigController
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                if (!SessionAuth())
                    return View();

                return RedirectToAction("Index", "Devices");
            }
            catch (Exception)
            {
                return View();
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login user)
        {
            
            try
            {
                if (!UserAuth(user))
                {
                    ModelState.AddModelError("Authentication error", "Username or password are wrong");
                    return View(user);
                }
                ModelState.AddModelError("Authentication error", "Logged in successfully");
                User User = db.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                SessionCookies(User);

                return RedirectToAction("Index", "Devices", user);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Authentication error", "Something went wrong, oops");
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                RemoveCookies();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Authentication error", "Something went wrong, oops");
                return RedirectToAction("Index", "Home");
            }
        }

    }
}