using GasStationRatingSystem.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStationRatingSystem.Web.Helper;
namespace GasStationRatingSystem.Web
{
    [Authorize()]

    public class ParentController : Microsoft.AspNetCore.Mvc.Controller
    {
        public JsonResult JsonDataTable(int total, object data) => JsonDataTable(new DataTableResponse(total, data));

        public JsonResult JsonDataTable(DataTableResponse res) => Json(res);
 
        
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string ControllerName = context.HttpContext.Request.RouteValues["controller"].ToString();
            string AreaName = context.HttpContext.Request.RouteValues["area"]?.ToString()?.ToLower() ?? "";
            string ActionName = context.HttpContext.Request.RouteValues["action"]?.ToString()?.ToLower() ?? "";

            if (ActionName != "reportdetails")
            {


                var userid = HttpContext.UserId();
                if (userid == null || userid == Guid.Empty)
                {

                    context.HttpContext.Response.Redirect("/Account/LogOff");
                    return;
                }

                var _pageBll = context.HttpContext.RequestServices.GetService(typeof(PageBll)) as PageBll;

                var HasPagePermission = _pageBll.GetPagePermission(ControllerName).Any();
                if (!HasPagePermission && ControllerName.ToLower() != "home" && ControllerName.ToLower() != "changepassword")
                {

                    context.HttpContext.Response.Redirect("/Home/UnAuthorize");
                }
            }
            base.OnActionExecuting(context);

        }

        
    }
}
