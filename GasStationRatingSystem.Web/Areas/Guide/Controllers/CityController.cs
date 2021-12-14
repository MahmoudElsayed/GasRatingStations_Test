using AutoMapper;
using GasStationRatingSystem;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO.Guide;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GasStationRatingSystem.Web.Areas.Guide
{
    public class CityController : Controller
    {
        #region Fields
        private readonly CityBll _CityBll;
        private readonly RegionBll _RegionBll;

        private readonly IMapper _mapper;
        public CityController(CityBll CityBll, RegionBll RegionBll, IMapper mapper)
        {
            _CityBll = CityBll;
            _RegionBll = RegionBll;
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
            return View(new CityDTO());

        }
        public IActionResult Save(CityDTO mdl) => Ok(_CityBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {

            var data = _CityBll.GetById(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }
                var tbl = _mapper.Map<CityDTO>(data);
                ViewData["Regions"] = _RegionBll.GetSelect();

                return View(tbl);

            }

            return View(new CityDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_CityBll.Delete(id));

        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_CityBll.LoadData(mdl));

        #endregion
    }
}
