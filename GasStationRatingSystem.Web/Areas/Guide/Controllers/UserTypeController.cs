using AutoMapper;
using GasStationRatingSystem;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO.Guide;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GasStationRatingSystem.Web.Areas.Guide
{
    public class UserTypeController : Controller
    {
        #region Fields
        private readonly UserTypeBll _UserTypeBll;
        private readonly IMapper _mapper;
        public UserTypeController(UserTypeBll UserTypeBll, IMapper mapper)
        {
            _UserTypeBll = UserTypeBll;
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
            return View(new UserTypeDTO());

        }
        public IActionResult Save(UserTypeDTO mdl) => Ok(_UserTypeBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {
            var data = _UserTypeBll.GetById(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }
                var tbl = _mapper.Map<UserTypeDTO>(data);

                return View(tbl);

            }

            return View(new UserTypeDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_UserTypeBll.Delete(id));

        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_UserTypeBll.LoadData(mdl));

        #endregion
    }
}
