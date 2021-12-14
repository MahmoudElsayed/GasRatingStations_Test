using AutoMapper;
using GasStationRatingSystem;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO.Guide;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GasStationRatingSystem.Web.Areas.Guide
{
    public class BranchController : Controller
    {
        #region Fields
        private readonly BranchBll _BranchBll;
        private readonly IMapper _mapper;
        public BranchController(BranchBll BranchBll, IMapper mapper)
        {
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
            return View(new BranchDTO());

        }
        public IActionResult Save(BranchDTO mdl) => Ok(_BranchBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {
            var data = _BranchBll.GetById(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }
                var tbl = _mapper.Map<BranchDTO>(data);

                return View(tbl);

            }

            return View(new BranchDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_BranchBll.Delete(id));

        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_BranchBll.LoadData(mdl));

        #endregion
    }
}
