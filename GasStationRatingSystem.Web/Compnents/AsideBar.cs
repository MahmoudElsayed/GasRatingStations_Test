
using GasStationRatingSystem.BLL;
using Microsoft.AspNetCore.Mvc;


namespace GasStationRatingSystem.Web.Compnents
{

    [ViewComponent(Name = "AsideBar")]
    public class AsideBar : ViewComponent
    {
        private readonly PageBll _pageBll;

        public AsideBar(PageBll pageBll) => _pageBll = pageBll;

        // _UserBll.GetAllowedUserAppForms()
        public IViewComponentResult Invoke() => View("Index", _pageBll.GetAsideBar());
    }
}
