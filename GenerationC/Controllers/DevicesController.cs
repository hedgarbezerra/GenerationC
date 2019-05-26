using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GenerationC.Database.models;

namespace GenerationC.Controllers
{
    public class DevicesController : ConfigController
    {
        
        // GET: Devices
        public ActionResult Index(string searchString)
        {
            if (!SessionAuth())
            {
                return RedirectToAction("Index", "Login");
            }
            int UserId = Current_user();

            if (!String.IsNullOrEmpty(searchString))
            {
                var devices = db.Devices.Where(d => d.User.Id == UserId && d.Name.Contains(searchString) ||  d.User.Id == UserId && d.Type.Contains(searchString));
                return View(devices.ToList());
            }

            else
            {
                var devices = db.Devices.Where(d => d.User.Id == UserId);
                return View(devices.ToList());
            }
            

        }

        // GET: Devices/Details/5
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

            Device device = db.Devices.Find(id);

            if (device == null)
            {
                return HttpNotFound();
            }
            if (device.User_Id != UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(device);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            if (!SessionAuth())
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Devices/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Device device)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int UserId = Current_user();
                    User user = db.Users.Find(UserId);
                    device.User = user;
                    device.User_Id = user.Id;
                    db.Devices.Add(device);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Devices");
                }
                catch
                {
                    return RedirectToAction("Create", "Devices");
                }
            }

            ViewBag.User_Id = new SelectList(db.Users, "Id", "Name", device.User_Id);
            return View(device);
        }

        // GET: Devices/Edit/5
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

            Device device = db.Devices.Find(id);

            if (device == null)
            {
                return HttpNotFound();
            }

            if(device.User_Id != UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            ViewBag.User_Id = new SelectList(db.Users, "Id", "Name", device.User_Id);
            return View(device);
        }

        // POST: Devices/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Device device)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Find(Current_user());
                device.Created_at = DateTime.Now;
                device.User = user;
                device.User_Id = user.Id;
                db.Entry(device).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.User_Id = new SelectList(db.Users, "Id", "Name", device.User_Id);
            return View(device);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!SessionAuth())
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Device device = db.Devices.Find(id);
            db.Devices.Remove(device);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
