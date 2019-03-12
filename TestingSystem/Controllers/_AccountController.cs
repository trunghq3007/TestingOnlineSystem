using System;

namespace TestingSystem.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using TestingSystem.Common;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="_AccountController" />
    /// </summary>
    public class _AccountController : Controller
    {
        /// <summary>
        /// Sets the success.
        /// </summary>
        /// <value>The success.</value>
        public string Success { set { TempData["Success"] = ViewData["Success"] = value; } }
        /// <summary>
        /// Sets the failure.
        /// </summary>
        /// <value>The failure.</value>
        public string Failure { set { TempData["Failure"] = ViewData["Failure"] = value; } }


       



        /// <summary>
        /// Defines the userService
        /// </summary>
        private IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="_AccountController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        public _AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// The Recovery
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Recovery()
        {
            return View();
        }


        /// <summary>
        /// The Recovery
        /// </summary>
        /// /// <param name="email">The key<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        [HttpPost]
        public ActionResult Recovery(string email)
        {
            userService.Recovery(email);
            return RedirectToAction("Logout");
        }

        public ActionResult Reset(string key)
        {
            string key1 = key;
            key1 = key1.Substring(0, key1.Length - 1);
            List<char> hashList = key1.ToCharArray().ToList();
            hashList.Reverse();
            string hash = "";
            foreach (var item in hashList)
                hash += item;
            key1 = Base64.Decode(hash);
            DateTime time = DateTime.Parse(key1.Split('_')[1]);
            if (time < DateTime.Now)
            {
                Response.StatusCode = 404;
                return View("Not found");
            }
            ViewBag.email = key;
            return View();
        }
        [HttpPost]
        public ActionResult Reset(string email, string pass, string comfirmpass)
        {
            if (pass != comfirmpass)
            {
                Failure = "Password does not same";
                ViewBag.email = email;
                return View();
            }
            email = email.Substring(0, email.Length - 1);
            List<char> hashList = email.ToCharArray().ToList();
            hashList.Reverse();
            string hash = "";
            foreach (var item in hashList)
                hash += item;
            email = Base64.Decode(hash);
            DateTime time = DateTime.Parse(email.Split('_')[1]);
            if (time < DateTime.Now)
            {
                Response.StatusCode = 404;
                return View("Not found");
            }
            email = email.Split('_')[0];
            userService.Reset(email, Encryptor.MD5Hash(comfirmpass));
            Success = "Reset your password success!";
            return RedirectToAction("Login", "Login");
        }



        /// <summary>
        /// The Verify
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Verify(string key)
        {
            key = Base64.Decode(key);
            if (userService.Active(key) == 1)
            {
                Success = "Active your account success!";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Response.StatusCode = 404;
                return View("Not found!");
            }
        }

        /// <summary>
        /// The Logout
        /// </summary>
        /// <returns>The <see cref="ActionResult"/></returns>
        public ActionResult Logout()
        {
            Session.Remove("Name");
            return RedirectToAction("Login", "Login");
        }
    }
}
