using AutoMapper;
using GasStationRatingSystem;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO.Guide;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GasStationRatingSystem.Web.Areas.Guide
{
    public class DistrictController : Controller
    {
        #region Fields
        private readonly DistrictBll _DistrictBll;
        private readonly RegionBll _RegionBll;
        private readonly CityBll _CityBll;

        private readonly IMapper _mapper;
        public DistrictController(DistrictBll DistrictBll, RegionBll RegionBll, CityBll CityBll, IMapper mapper)
        {
            _DistrictBll = DistrictBll;
            _RegionBll = RegionBll;
            _CityBll= CityBll;
            _mapper = mapper;   
        }
        #endregion
        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            ViewData["Regions"] = _RegionBll.GetSelect();
            return View(new DistrictDTO());

        }
        public IActionResult Save(DistrictDTO mdl) => Ok(_DistrictBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {

            var data = _DistrictBll.GetById(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }
                var tbl = _mapper.Map<DistrictDTO>(data);
                tbl.RegionId = data.City?.RegionId??Guid.Empty;
                ViewData["Regions"] = _RegionBll.GetSelect();

                return View(tbl);

            }

            return View(new DistrictDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_DistrictBll.Delete(id));

        #endregion

        #region Helpers
        public IActionResult LoadCities(Guid regionId)
        {
            return Ok(_CityBll.GetSelect(regionId));
        }
        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_DistrictBll.LoadData(mdl));

        #endregion
    }
}
