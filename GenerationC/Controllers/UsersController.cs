using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GenerationC.Database;
using GenerationC.Database.models;

namespace GenerationC.Controllers
{
    public class UsersController : ConfigController
    {

        public ActionResult Details(int? id)
        {
            int UserId = Current_user();

            if (!SessionAuth())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id != UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            
            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }
           
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            
            if (ModelState.IsValid)
            {

                if (UsernameExists(user))
                {
                    ModelState.AddModelError("IdentityError", "Username already existis");
                    return View(user);
                }

                if (EmailExists(user))
                {
                    ModelState.AddModelError("IdentityError", "Email already existis");
                    return View(user);
                }
                user.Password = ComputeHash(user.Password, null);

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Login"); 
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            int UserId = Current_user();

            if (!SessionAuth())
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (id != UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (UsernameExists(user))
                {
                    ModelState.AddModelError("IdentityError", "Username already existis");
                    return View(user);
                }

                if (EmailExists(user))
                {
                    ModelState.AddModelError("IdentityError", "Email already existis");
                    return View(user);
                }

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        protected bool UsernameExists(User user)
        {
            return db.Users.Any(u => u.Username == user.Username);
        }

        protected bool EmailExists(User user)
        {
            return db.Users.Any(u => u.Email == user.Email);
        }
    }
}
