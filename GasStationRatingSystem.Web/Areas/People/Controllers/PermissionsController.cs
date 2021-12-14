using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasStationRatingSystem.Web.Areas.People.Controllers
{
    public class PermissionsController : Controller
    {
        #region Fields

        private readonly UserBll _userBll;
        private readonly UserTypeBll _userTypeBll;  

        public PermissionsController(UserBll userBll,  UserTypeBll userTypeBll)
        {
            _userBll = userBll;
            _userTypeBll = userTypeBll;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            ViewBag.UserTypeId = new SelectList(_userTypeBll.GetSelect(), "Value", "Text");


            return View();
        }
   

        public IActionResult Save(UserDTO mdl) => Ok( _userBll.Save(mdl));

        




        #endregion
        #region Helpers
        public IActionResult ShowUserPermission(Guid userTypeId) => PartialView("_Permissions", _userBll.GetUserPermissions(userTypeId));
        [HttpPost]
        public IActionResult SaveUserPermission(Guid userTypeId, IEnumerable<UserPermissionsSaveDTO> mdl) => Ok(_userBll.SaveUserPermission(userTypeId, mdl));

        #endregion
    }
}
