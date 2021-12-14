using GasStationRatingSystem.BLL;
using GasStationRatingSystem.Common.General;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GasStationRatingSystem.Web.Areas.Setting.Controllers
{
    public class RatingsApproveController : Controller
    {
        private readonly VisitApprovalBll _visitApprovalBll;
        private readonly QuestionsBll _questionsBll;
        public RatingsApproveController(VisitApprovalBll visitApprovalBll, QuestionsBll questionsBll)
        {
            _visitApprovalBll = visitApprovalBll;
            _questionsBll = questionsBll;
        }
        public IActionResult Index()
        {
            //string SessionName = "lstQuestionsSession";
            //if (HttpContext.Session.GetObject<List<QuestionsDTO>>(SessionName) == null)
            //{
            //    var lstQuestions = _questionsBll.GetQuestions2();
            //    string query = "";
            //    int index = 1;
            //    lstQuestions.ForEach(p =>
            //     {
            //         query += $"{p.QuestionText}" + Environment.NewLine;
            //         //$"update [Guide].[Questions] set OrderNo={index++} where ID='{p.Id}'" + Environment.NewLine;

            //     }
            //         );



            //    HttpContext.Session.SetObject(SessionName, lstQuestions);

            //}

            return View();
        }


        #region Helpers
        public IActionResult ChangeCase(Guid visitNoId, ApprovalCasesEnum caseNo,string rejectedComment)
        {
            return Ok(_visitApprovalBll.ChangeCase(visitNoId,caseNo, rejectedComment));
        }
        public IActionResult GetQuestions(Guid visitNoId)
        {
            return PartialView("_StationQuestions", _visitApprovalBll.GetQuestions(visitNoId));
        }
        
        #endregion
        #region LoadData
        public IActionResult LoadDataTable(DataTableRequest mdl) => JsonDataTable(_visitApprovalBll.LoadData(mdl));

        #endregion
    }
}
