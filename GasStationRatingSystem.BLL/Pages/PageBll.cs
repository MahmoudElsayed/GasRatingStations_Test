using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
  public  class PageBll
    {
        #region Fields
        private readonly IRepository<Page> _repoPage;
        private readonly IRepository<UserPermission> _repoUserPermissions;
        private readonly IRepository<Area> _repoArea;
        private readonly UserBll _userBll;


        public PageBll(IRepository<Page> repoPage, IRepository<UserPermission> repoUserPermissions, IRepository<Area> repoArea, UserBll userBll)
        {
            _repoPage = repoPage;
            _repoUserPermissions = repoUserPermissions;
            _repoArea = repoArea;
            _userBll = userBll;
        }
        #endregion
        #region Actions
        public string GetPageName( string controllerName) => _repoPage.GetAll().Where(p => p.ControllerName == controllerName).FirstOrDefault()?.Text ?? "";
        public IEnumerable<UserPermission> GetPagePermission(string controllerName) => _repoUserPermissions.GetAll().Include(p=>p.ActionsPage).ThenInclude(p=>p.Page).Where(p => p.ActionsPage.Page.ControllerName.ToLower() == controllerName.ToLower()&&p.UserTypeId== _userBll.GetById(_repoArea.UserId).UserTypeId) ;

        public IEnumerable<AsideBarDTO> GetAsideBar()
        {
         
            var data = _repoArea.GetAll().Include(p => p.Pages).ThenInclude(p => p.ActionsPages).OrderBy(p => p.OrderNo).Select(p => new AsideBarDTO()
            {
                AreaTitle = p.Text,
                IconName=p.IconName,
                AsideBarPagesDTOs = p.Pages.OrderBy(p=>p.OrderNo).Select(p1 => new AsideBarPagesDTO()
                {
                    PageRoute =p1.ControllerName=="Xero"? $"{p.Name}/{p1.ControllerName}/Preparation" : $"{p.Name}/{p1.ControllerName}/Index",
                    PageTitle = p1.Text,
                    IconName = p1.IconName,
                    HasPermission = _repoUserPermissions.GetAll().Where(u => u.UserTypeId == _userBll.GetById(_repoArea.UserId).UserTypeId  && p1.ActionsPages.Contains(u.ActionsPage)).Any()

                })

            });
            return data.Where(p=>p.AsideBarPagesDTOs.Any(u=>u.HasPermission));
        }
        #endregion
    }
}
