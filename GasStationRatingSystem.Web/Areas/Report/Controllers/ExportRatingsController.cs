using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GasStationRatingSystem.BLL;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace GasStationRatingSystem.Web.Areas.Report.Controllers
{
    public class ExportRatingsController : Controller
    {
        #region Fields
        private readonly UserBll _userBll;
        private readonly BranchBll _branchBll;
        private readonly RegionBll _regionBll;
        private readonly CityBll _cityBll;
        private readonly DistrictBll _districtBll;
        private readonly GasStationBll  _gasStationBll;
        private readonly ExportRatingsBll _exportRatingsBll;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ExportRatingsController(UserBll userBll, BranchBll branchBll1, RegionBll regionBll, CityBll cityBll, DistrictBll districtBll, GasStationBll gasStationBll, ExportRatingsBll exportRatingsBll,IWebHostEnvironment hostingEnvironment)
        {
            _userBll = userBll;
            _branchBll = branchBll1;
            _regionBll = regionBll;
            _cityBll = cityBll;
            _districtBll = districtBll;
            _gasStationBll = gasStationBll;
            _exportRatingsBll = exportRatingsBll;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            ViewBag.Users = new SelectList(_userBll.GetSelect(), "Value", "Text");
            ViewBag.Branches = new SelectList(_branchBll.GetSelect(), "Value", "Text");

            return View();
        }

        #endregion

        #region Ajax
        [HttpPost]
        public IActionResult GetRegions(Guid[] branchIds) => Ok(_regionBll.GetSelect(branchIds));
        [HttpPost]
        public IActionResult GetCities(Guid?[] regionIds) => Ok(_cityBll.GetSelect(regionIds));
        [HttpPost]
        public IActionResult GetDistricts(Guid?[] cityIds) => Ok(_districtBll.GetSelect(cityIds));
        [HttpPost]
        public IActionResult GetStations(Guid ?[] districtIds) => Ok(_gasStationBll.GetStaionsByDistricts(districtIds));

        [HttpPost]
        public IActionResult Download(RatingsReportDTO mdl)
        {
            string fileName= Path.GetRandomFileName().Replace(".", "") + ".xlsx";
            string path = "ExportedFiles/" + fileName;
            string FullPath = $"{Path.Combine(_hostingEnvironment.WebRootPath,path)}";

            return Ok(new {data= _exportRatingsBll.ExportAnswers(mdl, FullPath),file=new {fileName,path  } } );
        }
        #endregion
    }
}
