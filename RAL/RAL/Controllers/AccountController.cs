using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using RAL.Models;
using RAL.Models.Interfaces;
using RAL_DAL;
using RAL.Infrastructure;

namespace RAL.Controllers
{
    public class AccountController : Controller
    {
        IRalRepository repository;

        public AccountController(IRalRepository _repository)
        {
            repository = _repository;
        }

        [Route("LogOn")]
        public ActionResult LogOn(LogOnViewModel logOnData)
        {
            /*
            try
            {
                if (ControllerContext.HttpContext.Session["curUser"] != null)
                {
                    return RedirectToActionPermanent("Index", "AHistory");
                }
            }
            catch { }
            */

            return View();
        }

        public ActionResult TryLogOn(LogOnViewModel logOnData)
        {
            User currentUser = getUserData(logOnData);
            string errorMessage = string.Empty;

            if (currentUser == null || !ModelState.IsValid)
            {
                errorMessage = "You entered invalid login or password. Try again.";
                return Json((object)errorMessage, JsonRequestBehavior.AllowGet);
            }

            setCurrentUser(currentUser, logOnData.rememberme);

            return Json((object)errorMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TryRegister(UserProfileViewModel userData)
        {
            string errorMessage = checkSignInErrors(userData);
            if (errorMessage != string.Empty || !ModelState.IsValid)
            {
                return Json((object)errorMessage, JsonRequestBehavior.AllowGet);
            }

            repository.addUser(userData);
            User currentUser = getUserData(new LogOnViewModel { login = userData.login, password = userData.password });
            setCurrentUser(currentUser, false);

            return Json((object)errorMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            CacheHelper.deleteCurrentUserCache(ControllerContext);

            return RedirectToAction("LogOn");
        }

        [NonAction]
        User getUserData(LogOnViewModel logOnData)
        {
            return repository.users.FirstOrDefault(u => u.login == logOnData.login && u.password == logOnData.password);
        }

        [NonAction]
        string checkSignInErrors(UserProfileViewModel userData)
        {
            if (!newUserLoginIsOriginal(userData))
            {
                return "User with such login has already exist. Please enter another login.";
            }
            if (!newUserEmailIsOriginal(userData))
            {
                return "User with such email has already exist. Please enter another email.";
            }
            return string.Empty;
        }

        [NonAction]
        bool newUserLoginIsOriginal(UserProfileViewModel userData)
        {
            if (repository.users.ToList<User>().Exists(u => u.login == userData.login))
            {
                return false;
            }

            return true;
        }

        [NonAction]
        bool newUserEmailIsOriginal(UserProfileViewModel userData)
        {
            if (repository.users.ToList<User>().Exists(u => u.email == userData.email))
            {
                return false;
            }

            return true;
        }

        [NonAction]
        void setCurrentUser(User currentUser, bool remember)
        {
            CacheHelper.cacheCurrentUser(currentUser, ControllerContext);

            var authTicket = new FormsAuthenticationTicket(
              1,
              currentUser.id.ToString(),  
              DateTime.Now,
              DateTime.Now.AddMinutes(20),  // expiry
              remember,  //true to remember
              "", //roles 
              "/"
            );

            //encrypt the ticket and add it to a cookie
            HttpCookie ticketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

            //my cookie - user id
            HttpCookie idCookie = new HttpCookie("userID", currentUser.id.ToString());

            Response.Cookies.Add(ticketCookie);
            Response.Cookies.Add(idCookie);
        }
	}
}