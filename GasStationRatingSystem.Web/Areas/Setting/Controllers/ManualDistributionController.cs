using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasStationRatingSystem.Web.Areas.Setting.Controllers
{
    public class ManualDistributionController : Controller
    {
        #region Fields

        private readonly UserBll _userBll;
        private readonly BranchBll _branchBll;
        private readonly RegionBll _regionBll;

        private readonly CityBll _cityBll;
        private readonly DistrictBll _districtBll;

        private readonly GasStationBll _gasStationBll;
        private readonly ManualDistributionBll _manualDistributionBll;

        public ManualDistributionController(UserBll userBll, BranchBll branchBll1, RegionBll regionBll, CityBll cityBll, DistrictBll districtBll, GasStationBll gasStationBll, ManualDistributionBll manualDistributionBll)
        {
            _userBll = userBll;
            _branchBll = branchBll1;
            _regionBll = regionBll;

            _cityBll = cityBll;
            _districtBll = districtBll;
            _gasStationBll = gasStationBll;
            _manualDistributionBll = manualDistributionBll;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            ViewBag.Branches = new SelectList(_branchBll.GetSelect(), "Value", "Text");

            return View();
        }
        public IActionResult Save(List< ManualDistributionDTO> mdl) => Ok(_manualDistributionBll.Save(mdl));
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_manualDistributionBll.Delete(id));
        [HttpPost]
        public IActionResult ChangeStatus(Guid id) => Ok(_manualDistributionBll.ChangeStatus(id));

        
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_manualDistributionBll.LoadData(mdl));




        #endregion
        #region Helpers
         #region Ajax
        [HttpPost]
        public IActionResult GetRegions(Guid? branchId) => Ok(_regionBll.GetSelect(branchId));
        [HttpPost]
        public IActionResult GetCities(Guid? regionId) => Ok(_cityBll.GetSelect(regionId));
        [HttpPost]
        public IActionResult GetDistricts(Guid? cityId) => Ok(_districtBll.GetSelect(cityId));

      
        #endregion
        [HttpPost]
        public IActionResult LoadUsers(Guid ?regionId) => Ok(_userBll.GetUsers(regionId));
        [HttpPost]
        public IActionResult LoadStaions(Guid districtId,Guid?stationId=null) => Ok(_gasStationBll.GetStaionsNotDistributed(districtId,stationId));

        
        #endregion
    }
}
