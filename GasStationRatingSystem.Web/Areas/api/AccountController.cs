using GasStationRatingSystem.BLL;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static GasStationRatingSystem.Common.AppConstants;

namespace GasStationRatingSystem.Web.Areas.api
{
    public class AccountController : BaseController
    {

        #region Fields
        private readonly IJWTAuthentication _jWTAuthentication;

        private readonly UserBll _userBll;
        public AccountController(UserBll userBll, IJWTAuthentication jWTAuthentication)
        {
            _userBll = userBll;
            _jWTAuthentication = jWTAuthentication;
        }
        #endregion
        #region Login
        [HttpPost, AllowAnonymous]

        public IActionResult Login(UserParameters para)
        {
            ResultDTO result = new ResultDTO();
          
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                result.Message =HttpContext.GetLocalizedString( errors[0].FirstOrDefault().ErrorMessage);
                return StatusCode( AppConstants.StatusCodes.Error ,result);

            }
            var data = _userBll.Login(para);
            if (data.data!=null)
            {
                var UserObject = data.data.GetType().GetProperties().FirstOrDefault();
                var user = UserObject.GetValue(data.data) as UserResultDTO;
                var Token = _jWTAuthentication.Authenticate(user.Id.ToString());
                user.BearerToken = Token;
                
                 
                return StatusCode( AppConstants.StatusCodes.Success, data);

            }


            return StatusCode( AppConstants.StatusCodes.Error , data);
        }
        #endregion
        #region ChangePassword
        [HttpPost]
        public IActionResult ChangePassword(UserChangePasswordParameters para)
        {
            ResultDTO result = new ResultDTO();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                result.Message = HttpContext.GetLocalizedString(errors[0].FirstOrDefault().ErrorMessage);
                return StatusCode(AppConstants.StatusCodes.Error, result);

            }
            var data = _userBll.ChangePassword(para);
            if (data.data != null)
            {
                
                return StatusCode(AppConstants.StatusCodes.Success, data);

            }


            return StatusCode(AppConstants.StatusCodes.Error, data);
        }

        #endregion
        #region Forget Password
        [HttpPost, AllowAnonymous]
        public IActionResult SendCode(UserSendCodeParameters para)
        {
            ResultDTO result = new ResultDTO();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                result.Message = HttpContext.GetLocalizedString(errors[0].FirstOrDefault().ErrorMessage);
                return StatusCode(AppConstants.StatusCodes.Error, result);

            }
            var data = _userBll.SendCode(para);
            if (data.data != null)
            {

                return StatusCode(AppConstants.StatusCodes.Success, data);

            }


            return StatusCode(AppConstants.StatusCodes.Error, data);
        }
        [HttpPost, AllowAnonymous]
        public IActionResult ForgetPassword(UserForgetPasswordParameters para)
        {
            ResultDTO result = new ResultDTO();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                result.Message = HttpContext.GetLocalizedString(errors[0].FirstOrDefault().ErrorMessage);
                return StatusCode(AppConstants.StatusCodes.Error, result);

            }
            var data = _userBll.ForgetPassword(para);
            if (data.data != null)
            {

                return StatusCode(AppConstants.StatusCodes.Success, data);

            }


            return StatusCode(AppConstants.StatusCodes.Error, data);
        }

        #endregion
        [NonAction]
        public IActionResult GetUsers()
        {
            return StatusCode(AppConstants.StatusCodes.Error, new {data= HttpContext.LanguageIsArabic() });
        }
    }
}
