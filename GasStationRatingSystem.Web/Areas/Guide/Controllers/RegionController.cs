using AutoMapper;
using GasStationRatingSystem;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO.Guide;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GasStationRatingSystem.Web.Areas.Guide
{
    public class RegionController : Controller
    {
        #region Fields
        private readonly RegionBll _RegionBll;
        private readonly BranchBll  _BranchBll;

        private readonly IMapper _mapper;
        public RegionController(RegionBll RegionBll, BranchBll BranchBll, IMapper mapper)
        {
            _RegionBll = RegionBll;
            _BranchBll = BranchBll;
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
            ViewData["Branches"] = _BranchBll.GetSelect();
            return View(new RegionDTO());

        }
        public IActionResult Save(RegionDTO mdl) => Ok(_RegionBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {

            var data = _RegionBll.GetById(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }
                var tbl = _mapper.Map<RegionDTO>(data);
                ViewData["Branches"] = _BranchBll.GetSelect();

                return View(tbl);

            }

            return View(new RegionDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_RegionBll.Delete(id));

        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_RegionBll.LoadData(mdl));

        #endregion
    }
}
