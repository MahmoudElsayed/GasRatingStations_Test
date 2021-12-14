using GasStationRatingSystem;
using GasStationRatingSystem.BLL;

using Microsoft.AspNetCore.Mvc;

namespace Electricity.Web.Compnents
{
    [ViewComponent(Name = nameof(UserDetailsLi))]
    public class UserDetailsLi : ViewComponent
    {
        private readonly UserBll _userBll;

        public UserDetailsLi(UserBll userBll) => _userBll = userBll;

        public IViewComponentResult Invoke() => View("Index", _userBll.GetById(HttpContext.UserId()));

    }
}
