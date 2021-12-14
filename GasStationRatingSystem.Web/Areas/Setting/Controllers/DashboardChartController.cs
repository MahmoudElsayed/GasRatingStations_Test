using AutoMapper;
using GasStationRatingSystem;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.DTO.Guide;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace GasStationRatingSystem.Web.Areas.Setting.Controllers
{
    public class DashboardChartController : Controller
    {
        #region Fields
        private readonly DashboardChartBll _DashboardChartBll;
        private readonly QuestionsBll _QuestionsBll;

        private readonly IMapper _mapper;
        public DashboardChartController(DashboardChartBll DashboardChartBll, QuestionsBll QuestionsBll, IMapper mapper)
        {
            _DashboardChartBll = DashboardChartBll;
            _QuestionsBll = QuestionsBll;
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
            ViewBag.Questions = new SelectList(_QuestionsBll.GetDashboardSettingSelect(), "Value", "Text");

            return View(new DashboardChartDTO());

        }
        public IActionResult Save(DashboardChartDTO mdl) => Ok(_DashboardChartBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {
            var data = _DashboardChartBll.GetById(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }


            //    var tbl = _mapper.Map<DashboardChartDTO>(data);
              
                var tbl=new DashboardChartDTO() { ID=data.ID, Title=data.Title, QuestionsIdsArray=data.QuestionsIds?.Split(',')};
                ViewBag.Questions = new SelectList(_QuestionsBll.GetDashboardSettingSelect(), "Value", "Text",tbl.QuestionsIds);

                return View(tbl);

            }

            return View(new DashboardChartDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_DashboardChartBll.Delete(id));

        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_DashboardChartBll.LoadData(mdl));

        #endregion
    }
}
