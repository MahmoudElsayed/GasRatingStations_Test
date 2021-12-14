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
    public class UserController : Controller
    {
        #region Fields

        private readonly UserBll _userBll;
        private readonly UserTypeBll _userTypeBll;  
        private readonly RegionBll _regionBll;

        public UserController(UserBll userBll, RegionBll regionBll, UserTypeBll userTypeBll)
        {
            _userBll = userBll;
            _regionBll = regionBll;
            _userTypeBll = userTypeBll;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            ViewBag.RegionIds =new SelectList(_regionBll.GetSelect(),"Value","Text");
            ViewBag.UserTypeId = new SelectList(_userTypeBll.GetSelect(), "Value", "Text");

            return View();
        }
        public IActionResult Permissions()
        {
            return View();
        }
        public IActionResult Save(UserDTO mdl) => Ok( _userBll.Save(mdl));
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_userBll.Delete(id));
        [HttpPost]
        public IActionResult ChangeStatus(Guid id) => Ok(_userBll.ChangeStatus(id));

        [HttpPost]
        public IActionResult ResetPassword(Guid id) => Ok(_userBll.ResetPassword(id));

        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_userBll.LoadData(mdl));




        #endregion

    }
}
