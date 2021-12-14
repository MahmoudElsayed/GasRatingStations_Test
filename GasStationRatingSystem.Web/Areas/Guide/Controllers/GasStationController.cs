using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GasStationRatingSystem.Web.Areas.Guide
{
    public class GasStationController : Controller
    {
        #region Fields
        private readonly GasStationBll _gasStationBll;
        private readonly BranchBll _branchBll;
        private readonly RegionBll _regionBll;
        private readonly CityBll _cityBll;
        private readonly DistrictBll _districtBll;

        public GasStationController(GasStationBll gasStationBll,  BranchBll branchBll1, RegionBll regionBll, CityBll cityBll, DistrictBll districtBll)
        {
            _gasStationBll = gasStationBll;
            _branchBll = branchBll1;
            _regionBll = regionBll;
            _cityBll = cityBll;
            _districtBll = districtBll;

        }
        #endregion
        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {

            ViewBag.Branches = new SelectList(_branchBll.GetSelect(), "Value", "Text");

            return View(new GasStationDTO());

        }
        public IActionResult Save(GasStationDTO mdl) => Ok(_gasStationBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {
            var data = _gasStationBll.GetByIdWithAddtionalData(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }
                var config = new MapperConfiguration(p => p.CreateMap<GasStation, GasStationDTO>());
                var mapper = new Mapper(config);
                var tbl = mapper.Map<GasStationDTO>(data);
                tbl.BranchId = data.District.City.Region.Branch.ID;
                tbl.RegionId = data.District.City.Region.ID;
                tbl.CityId = data.District.City.ID;
                tbl.DistrictId = data.DistrictId;




                ViewBag.Branches = new SelectList(_branchBll.GetSelect(), "Value", "Text",tbl.BranchId);

                return View(tbl);

            }

            return View(new GasStationDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_gasStationBll.Delete(id));

        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl,int gasStationVisitStatus) => JsonDataTable(_gasStationBll.LoadData(mdl,gasStationVisitStatus));

        #endregion
        #region Ajax
        [HttpPost]
        public IActionResult GetRegions(Guid? branchId) => Ok(_regionBll.GetSelect(branchId));
        [HttpPost]
        public IActionResult GetCities(Guid? regionId) => Ok(_cityBll.GetSelect(regionId));
        [HttpPost]
        public IActionResult GetDistricts(Guid? cityId) => Ok(_districtBll.GetSelect(cityId));

      
        #endregion
    }
}
