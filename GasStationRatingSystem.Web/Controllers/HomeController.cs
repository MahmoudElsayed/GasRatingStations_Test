using ClosedXML.Excel;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using GasStationRatingSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationRatingSystem.Web.Controllers
{
    public class HomeController : ParentController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DashboardBll _dashboardBll;

        private readonly UserBll _userBll;
        private readonly QuestionsBll _questionsBll;
        private readonly IRepository<VisitInfo> _repoVisitInfo;
        private readonly IRepository<VisitAnswerOption> _repoVisitAnswerOption;
        private readonly IRepository<Question> _repoQuestion;


        public class QDto
        {
            public Guid Id { get; set; }
            public string Text { get; set; }
        }
        public HomeController(ILogger<HomeController> logger, DashboardBll dashboardBll, UserBll userBll, QuestionsBll questionsBll, IRepository<VisitInfo> repoVisitInfo, IRepository<VisitAnswerOption> repoVisitAnswerOption
            , IRepository<Question> repoQuestion

            )
        {
            _logger = logger;
            _dashboardBll = dashboardBll;
            _userBll = userBll;
            _questionsBll = questionsBll;
            _repoVisitInfo = repoVisitInfo;
            _repoVisitAnswerOption = repoVisitAnswerOption;
            _repoQuestion = repoQuestion;
          
        }
    public class DataDTO
    {
        public string    Text { get; set; }
    }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ChangePassword() => View();

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult UnAuthorize() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Helpers
        [HttpPost]
        public JsonResult ChangeOldPassword(ChangePasswordDTO mdl) => Json(_userBll.ChangeOldPasswordWeb(mdl));

        public IActionResult LoadTotals(DateTime? dateFrom, DateTime? dateTo) => Ok( _dashboardBll.GetTotals(dateFrom,dateTo));
        public IActionResult LoadCitiesSations(DateTime? dateFrom, DateTime? dateTo) => Ok(_dashboardBll.GetCitiesSations(dateFrom,dateTo));

        public IActionResult GetDashboardCharts(DateTime? dateFrom, DateTime? dateTo) => Ok(_dashboardBll.GetDashboardCharts(dateFrom,dateTo));

        
        #endregion
    }
}
