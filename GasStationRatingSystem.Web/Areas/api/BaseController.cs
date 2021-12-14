using GasStationRatingSystem.Common;
using GasStationRatingSystem.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationRatingSystem.Web.Areas.api
{
    [Area(AppConstants.Areas.api)]
    [ApiController]
    [Route("[area]/[controller]/[action]")]
    [Authorize]

    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-SA");
        }
       
    }
}
