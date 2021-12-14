using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GasStationRatingSystem.Web.Areas.Guide.Controllers
{
    public class QuestionController : Controller
    {
        private readonly QuestionsBll _questionsBll;

        public QuestionController(QuestionsBll questionsBll)
        {
            _questionsBll = questionsBll;
            var a = _questionsBll.GetQuestions2();
        }
        public IActionResult Index()
        {
            ViewBag.AnswersCategories = new SelectList(_questionsBll.GetAnswersCategories(), "Value", "Text");
            return View();
        }
        public IActionResult DeleteQuestionFromTreeView(Guid questionId) => Ok(_questionsBll.DeleteQuestionFromTreeView(questionId));
        
        public IActionResult CheckAddChild(Guid parentId) => Ok(_questionsBll.CheckAddChild(parentId));

        public IActionResult Save(QuestionSaveDTO question) => Ok(_questionsBll.Save(question));

      
        #region GetQuestionsTreeView

        public IActionResult GetQuestionsTreeView() => Ok(_questionsBll.GetQuestionsTreeView());
        #endregion

        #region QuestionsForOrder
        public IActionResult GetQuestionsForOrder(Guid? id) => PartialView("_QuestionsOrder", _questionsBll.GetQuestionsByParentId(id));
        public IActionResult SaveQuestionsOrder(List<QuestionsOrderDTO> mdl) => Ok(_questionsBll.SaveQuestionsOrder(mdl));

        
        #endregion

    }
}
