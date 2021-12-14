using GasStationRatingSystem.BLL;
using GasStationRatingSystem.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationRatingSystem.Web.Areas.api
{
    public class QuestionsController : BaseController
    {
        #region Fields

        private readonly QuestionsBll _questionsBll;

        public QuestionsController(QuestionsBll questionsBll)
        {
            _questionsBll = questionsBll;
        }
        #endregion
        [HttpGet]
        public IActionResult GetQuestions()
        {
            return StatusCode(AppConstants.StatusCodes.Success,_questionsBll.GetQuestions() );
        }
        [HttpGet]
        public IActionResult GetQuestionsByVisit(Guid visitId)
        {
            return StatusCode(AppConstants.StatusCodes.Success, _questionsBll.GetQuestionsByVisit(visitId));
        }
        [HttpGet]
        public IActionResult GetAnsweredQuestions(Guid stationId)
        {
            return StatusCode(AppConstants.StatusCodes.Success, _questionsBll.GetAnsweredQuestions(stationId));
        }
        [HttpGet]
        public IActionResult GetPendingQuestionsByVisit(Guid visitId)
        {
            return StatusCode(AppConstants.StatusCodes.Success, _questionsBll.GetPendingQuestions(visitId));
        }
        [HttpGet]
        public IActionResult GetVisits()
        {
            return StatusCode(AppConstants.StatusCodes.Success, _questionsBll.GetVisits());
        }



    }
}
