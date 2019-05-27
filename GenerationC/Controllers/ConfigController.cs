using GenerationC.Database;
using GenerationC.Database.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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


        private void WriteCookie(string CookieName, string value)
        {
            HttpCookie cookie = new HttpCookie(CookieName)
            {
                Value = value
            };
            TimeSpan exp = new TimeSpan(0, 12, 0, 0);
            cookie.Expires = DateTime.Now + exp;
            Response.Cookies.Add(cookie);
        }

        protected void RemoveCookies()
        {   
            string[] allDomainCookes = HttpContext.Request.Cookies.AllKeys;
            foreach (string domainCookie in allDomainCookes)
            {
                var expiredCookie = new HttpCookie(domainCookie)
                {
                    Expires = DateTime.Now.AddDays(-7),
                };
                HttpContext.Response.Cookies.Add(expiredCookie);
            }
            HttpContext.Session.Abandon();
            HttpContext.Request.Cookies.Clear();
        }

        protected bool SessionAuth()
        {
            string[] allDomainCookes = HttpContext.Request.Cookies.AllKeys;
            foreach (string domainCookie in allDomainCookes)
            {
                if (domainCookie.Contains("Id"))
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


        protected void SessionCookies(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
            WriteCookie("Id", user.Id.ToString());
            WriteCookie("Username", user.Username.ToString());
            WriteCookie("Name", user.Name.ToString());

        }

        protected static string ComputeHash(string plainText,
                                     byte[] saltBytes)
        {
            if (saltBytes == null)
            {
                int minSaltSize = 4;
                int maxSaltSize = 8;

                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                saltBytes = new byte[saltSize];

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                rng.GetNonZeroBytes(saltBytes);
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash = new SHA256Managed();


            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }


        protected static bool VerifyHash(string plainText,
                                      string hashValue)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits = 256;
            int hashSizeInBytes;

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString =
                        ComputeHash(plainText, saltBytes);

            return (hashValue == expectedHashString);
        }


    }

}