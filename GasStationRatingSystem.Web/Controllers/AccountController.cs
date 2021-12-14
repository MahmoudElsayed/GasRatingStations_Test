using GasStationRatingSystem.BLL;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationRatingSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserBll _userBll;
        public AccountController(UserBll userBll)
        {
            _userBll = userBll;
        }
        public IActionResult Login()
        {
           // HttpContext.Response.Redirect("/Account/Index");
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ChangePassword() => View();
      
        [HttpPost]
        public IActionResult LogIn(LogInDTO mdl, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userBll.LogInWeb(mdl);
                if (user != null)
                {
                    HttpContext.Response.Cookies.AppendCookie(AppConstants._UserIdCookie, user.ID.ToString(), true);

                    if (!returnUrl.IsEmpty())
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("~/Home/Index");
                }
            }

            return View(mdl);
        }
        [HttpPost]
        public JsonResult ChangeOldPassword(ChangePasswordDTO mdl) => Json(_userBll.ChangeOldPasswordWeb(mdl));
        public IActionResult LogOff(string returnUrl)
        {
            if (Response.HttpContext.Request.Cookies.ContainsKey(AppConstants._UserIdCookie))
            {
                var uid = Guid.Parse(Response.HttpContext.Request.Cookies[AppConstants._UserIdCookie]);
                Response.Cookies.Delete(AppConstants._UserIdCookie);
            }
            return Redirect(Url.GetAction(nameof(LogIn)) + (returnUrl.IsEmpty() ? "" : "returnUrl=" + returnUrl));
        }
    }

}
