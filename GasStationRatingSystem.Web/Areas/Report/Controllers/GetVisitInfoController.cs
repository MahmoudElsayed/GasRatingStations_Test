using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasStationRatingSystem.Web.Areas.Report.Controllers
{
    public class GetVisitInfoController : Controller
    {
        #region Fields
        private readonly UserBll _userBll;
        private readonly QuestionsBll  _questionsBll;
        private readonly BranchBll _branchBll;
        private readonly RegionBll _regionBll;
        private readonly CityBll _cityBll;
        private readonly DistrictBll _districtBll;


        public GetVisitInfoController(UserBll userBll, QuestionsBll questionsBll,BranchBll branchBll1,RegionBll regionBll, CityBll cityBll, DistrictBll districtBll)
        {
            _userBll= userBll;  
            _questionsBll=questionsBll;
            _branchBll = branchBll1;
            _regionBll = regionBll;
            _cityBll = cityBll;
            _districtBll = districtBll;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            ViewBag.Users = new SelectList( _userBll.GetSelect(),"Value","Text");
            ViewBag.Questions = new SelectList(_questionsBll.GetSelect(), "Value", "Text");
            ViewBag.Branches = new SelectList(_branchBll.GetSelect(), "Value", "Text");

            return View();
        }

        #endregion

        #region Ajax
        [HttpPost]
        public IActionResult GetRegions(Guid[] branchIds) => Ok( _regionBll.GetSelect(branchIds));
        [HttpPost]
        public IActionResult GetCities(Guid ? []regionIds) => Ok(_cityBll.GetSelect(regionIds));
        [HttpPost]
        public IActionResult GetDistricts(Guid?[] cityIds) => Ok(_districtBll.GetSelect(cityIds));
  
        [HttpPost]
        public IActionResult View(RatingsReportDTO mdl)
        {
            return PartialView("_Report", mdl);
        }
        #endregion
    }
}
