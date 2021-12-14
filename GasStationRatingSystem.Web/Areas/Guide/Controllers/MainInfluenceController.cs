using AutoMapper;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace GasStationRatingSystem.Web.Areas.Guide.Controllers
{
    public class MainInfluenceController : Controller
    {
        #region Fields
        private readonly MainInfluenceBll _MainInfluenceBll;
        private readonly QuestionsBll _QuestionsBll;

        private readonly IMapper _mapper;
        public MainInfluenceController(MainInfluenceBll MainInfluenceBll, QuestionsBll QuestionsBll, IMapper mapper)
        {
            _MainInfluenceBll = MainInfluenceBll;
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
            ViewBag.Questions = new MultiSelectList(_QuestionsBll.GetAll(), "ID", "Text");
            return View(new MainInfluenceDTO());

        }
        public IActionResult Save(MainInfluenceDTO mdl) => Ok(_MainInfluenceBll.Save(mdl));

        public IActionResult Edit(Guid id)
        {
            var data = _MainInfluenceBll.GetById(id);
            if (data != null)
            {
                if (data.ID == Guid.Empty)
                {
                    return Redirect(Url.GetAction(nameof(Index)));
                }
                var tbl = _mapper.Map<MainInfluenceDTO>(data);

                var QuestionIds = data.MainInfluenceQuestions
                    .Select(p => p.QuestionId.ToString());
                ViewBag.Questions = new MultiSelectList(_QuestionsBll.GetAll(), "ID", "Text", QuestionIds);

                return View(tbl);

            }

            return View(new MainInfluenceDTO());
        }
        [HttpPost]
        public IActionResult Delete(Guid id) => Ok(_MainInfluenceBll.Delete(id));

        #endregion

        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_MainInfluenceBll.LoadData(mdl));

        #endregion
    }
}
