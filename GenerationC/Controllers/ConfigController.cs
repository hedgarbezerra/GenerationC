using GenerationC.Database;
using GenerationC.Database.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GenerationC.Controllers
{
    public class ConfigController : Controller
    {
        //GLOBAL VARS
        protected GenerationCDbContextC db;

        //DATABASE CONNECTION
        public ConfigController()
        {
            db = new GenerationCDbContextC();
        }


        private void WriteCookie(string nomeCookie, string valor)
        {
            HttpCookie cookie = new HttpCookie(nomeCookie)
            {
                Value = valor
            };
            TimeSpan tempo = new TimeSpan(1, 0, 0, 0);
            cookie.Expires = DateTime.Now + tempo;
            Response.Cookies.Add(cookie);
        }

        protected void RemoveCookies()
        {
            string[] allDomainCookes = HttpContext.Request.Cookies.AllKeys;
            foreach (string domainCookie in allDomainCookes)
            {
                var expiredCookie = new HttpCookie(domainCookie)
                {
                    Expires = DateTime.Now.AddDays(-5),
                };
                HttpContext.Response.Cookies.Add(expiredCookie);
            }
            HttpContext.Request.Cookies.Clear();
        }

        protected bool SessionAuth()
        {
            string[] allDomainCookes = HttpContext.Request.Cookies.AllKeys;
            foreach (string domainCookie in allDomainCookes)
            {
                if (domainCookie.Contains(".ASPXAUTH"))
                {
                    return true;
                }
            }
            return false;
        }

        public int Current_user()
        {
            return Convert.ToInt32(Request.Cookies["Id"].Value.ToString());
        }

        protected bool UserAuth(Login user)
        {
            return db.Users.Any(u => u.Username == user.Username && u.Password == user.Password);
        }

        protected void SessionCookies(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
            WriteCookie("Id", user.Id.ToString());
            WriteCookie("Username", user.Username.ToString());
            WriteCookie("Name", user.Name.ToString());

        }
    }
}