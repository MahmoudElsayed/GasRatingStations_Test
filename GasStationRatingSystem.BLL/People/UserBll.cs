using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.Common.General;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
    public class UserBll
    {
        #region Fields
        private const string _spUsers = "People.[spUsers]";

        private readonly IRepository<User> _repoUser;
        private readonly IRepository<UserRegion> _repoUserRegion;
        private readonly IRepository<UserPermission> _repoUserPermission;
        private readonly IRepository<Page> _repoPage;
        private readonly IRepository<ActionsPage> _repoActionsPage;
        private readonly CityBll  _cityBll;

        public UserBll(IRepository<User> repoUser, IRepository<UserRegion> repoUserRegion,
            IRepository<UserPermission> repoUserPermission, IRepository<Page> repoPage,
            IRepository<ActionsPage> repoActionsPage,
            CityBll cityBll
            )
        {
            _repoUser = repoUser;
            _repoUserRegion = repoUserRegion;
            _repoUserPermission = repoUserPermission;
            _repoPage = repoPage;
            _repoActionsPage = repoActionsPage;
            _cityBll = cityBll;
        }

        #endregion
        #region Get
        public ResultDTO Get()
        {
            ResultDTO result = new ResultDTO();
            result.data = new
            {
                Users = _repoUser.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted && p.ID != _repoUser.UserId).Select(p => new
                {
                    Id = p.ID,
                    Name = p.UserName
                })
            };
            return result;
        }
        public IQueryable<SelectListDTO> GetSelect()
        {
            var userRegions = _repoUserRegion.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive&&p.UserId==_repoUser.UserId).Select(p=>p.RegionId);
            return _repoUser.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive&&p.UserRegions.Any(r=>userRegions.Any(r1=>r1==r.RegionId))).Select(p => new SelectListDTO()
            {
                Value = p.ID,
                Text = p.UserName
            });
        }
        
        public User GetById(Guid id)
        {

            return _repoUser.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
        }
        #endregion

        #region Login
        public ResultDTO Login(UserParameters para)
        {
            ResultDTO result = new ResultDTO();
            var data = _repoUser.GetAll().Where(p => (p.UserName.ToLower() == para.Username.ToLower() || p.Email.ToLower() == para.Username.ToLower()) && p.PasswordHash == para.Password.EncryptString()).FirstOrDefault();
            if (data != null)
            {

                result.data = new { User = new UserResultDTO { Id = data.ID, Username = data.UserName, UseDefaultPassword = data.UseDefaultPassword } };
            }
            else
            {
                result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorInUsernameOrPassword));
            }
            return result;

        }
        #endregion
        #region Change Password
        public ResultDTO ChangePassword(UserChangePasswordParameters para)
        {
            ResultDTO result = new ResultDTO();
            var data = _repoUser.GetAllAsNoTracking().Where(p => p.ID == _repoUser.UserId).FirstOrDefault();
            if (data != null)
            {
                data.PasswordHash = para.NewPassword.EncryptString();
                data.UseDefaultPassword = false;
                if (_repoUser.Update(data))
                {
                    result.data = new { Successed = true };

                    result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                }
                else
                {
                    result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                }
            }
            else
            {
                result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.UserNotExists));
            }
            return result;

        }
        #endregion
        #region Forget Password
        public ResultDTO SendCode(UserSendCodeParameters para)
        {
            ResultDTO result = new ResultDTO();
            var data = _repoUser.GetAll().Where(p => (p.UserName.ToLower() == para.Username.ToLower() || p.Email.ToLower() == para.Username.ToLower())).FirstOrDefault();
            if (data != null)
            {
                var _SendBll = _repoUser.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(SendBll)) as SendBll;
                string Code = new Random((int)DateTime.Now.Ticks).Next(11111, 99999) + "";
                if (_SendBll.SendMail("كود تعيين كلمة المرور", data.UserName, $"كود التحقق  {Code}", data.Email))
                {
                    data.ResetPasswordDate = AppDateTime.Now;
                    data.CodeOfReset = Code;
                    if (_repoUser.Update(data))
                    {
                        result.data = new { Successed = true };

                        result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                    }
                    else
                    {
                        result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                    }
                }
                else
                {
                    result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                }
            }
            else
            {
                result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.UserNotExists));
            }
            return result;

        }
        public ResultDTO ForgetPassword(UserForgetPasswordParameters para)
        {
            ResultDTO result = new ResultDTO();
            var data = _repoUser.GetAll().Where(p => (p.UserName.ToLower() == para.Username.ToLower() || p.Email.ToLower() == para.Username.ToLower())).FirstOrDefault();
            if (data != null)
            {
                if (data.CodeOfReset != para.Code)
                {
                    result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ValidationCodeInCorrect));

                }
                else
                {
                    data.PasswordHash = para.NewPassword.EncryptString();
                    data.CodeOfReset = null;
                    data.ResetPasswordDate = null;
                    if (_repoUser.Update(data))
                    {
                        result.data = new { Successed = true };

                        result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                    }
                    else
                    {
                        result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                    }
                }
            }
            else
            {
                result.Message = _repoUser.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.UserNotExists));
            }
            return result;

        }

        #endregion

        #region Web
        public ResultViewModel Save(UserDTO userDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };
            var config = new MapperConfiguration(p => p.CreateMap<UserDTO, User>());
            var mapper = new Mapper(config);
            var user = mapper.Map<User>(userDTO);
            var tbl = _repoUser.GetAllAsNoTracking().Where(p => p.ID == userDTO.ID).Include(p => p.UserRegions).FirstOrDefault();
            if (tbl == null)
            {
                if (_repoUser.GetAll().Where(p => p.UserName.Trim().ToLower() == userDTO.UserName.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.UserMessages.UsernameAlreadyExists;
                    return resultViewModel;

                }

                if (_repoUser.GetAll().Where(p => p.Email.Trim().ToLower() == userDTO.Email.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.UserMessages.EmailAlreadyExists;
                    return resultViewModel;
                }

                user.PasswordHash = AppConstants.DefaultPassword.EncryptString();
                user.Salt = AppConstants.EncryptKey;

                user.UserRegions = userDTO.RegionIds.Select(p => new UserRegion()
                {
                    AddedBy = _repoUser.UserId,
                    UserId = user.ID,
                    RegionId = p,

                }).ToList();

                if (_repoUser.Insert(user))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoUser.GetAll().Where(p => p.ID != tbl.ID && p.UserName.Trim().ToLower() == userDTO.UserName.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.UserMessages.UsernameAlreadyExists;
                    return resultViewModel;

                }

                if (_repoUser.GetAll().Where(p => p.ID != tbl.ID && p.Email.Trim().ToLower() == userDTO.Email.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.UserMessages.EmailAlreadyExists;
                    return resultViewModel;
                }

                tbl.UserName = user.UserName;
                tbl.Email = user.Email;
                tbl.IsActive = user.IsActive;
                tbl.UserTypeId = user.UserTypeId;
                tbl.InspectorName = user.InspectorName;




                // tbl.CityId = user.CityId;
                if (_repoUser.Update(tbl))
                {
                    _repoUser.ExecuteStoredProcedure<int>($"delete from People.UserRegions where UserId='{tbl.ID}'", null, CommandType.Text);
                    _repoUserRegion.InsertRange(userDTO.RegionIds.Select(p => new UserRegion()
                    {
                        AddedBy = _repoUser.UserId,
                        UserId = user.ID,
                        RegionId = p,

                    }).ToList());
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }





            return resultViewModel;
        }
        public ResultViewModel Delete(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoUser.GetById(id);
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoUser.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }
        public ResultViewModel ChangeStatus(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoUser.GetById(id);
            tbl.IsActive = !tbl.IsActive;
            var IsSuceess = _repoUser.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.ChangedStatusSuccess : AppConstants.Messages.ChangedStatusFailed;


            return resultViewModel;
        }
        public ResultViewModel ResetPassword(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoUser.GetById(id);
            tbl.PasswordHash = "hRbpM+HWb0R3yEmWMCATPw==";
            var IsSuceess = _repoUser.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? "تم استعادة كلمة المرور الافتراضية" : "حدث خطا اثناء اجراء العملية";


            return resultViewModel;
        }
        
        #region LoadData
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoUser.ExecuteStoredProcedure<UserDTO>
                (_spUsers, mdl?.ToSqlParameter(_repoUser.UserId), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        #region Login For Web
        #region  تسجيل الدخول
        public User LogInWeb(LogInDTO mdl)
        {
            if (mdl != null && !mdl.UserCode.IsEmpty() && !mdl.Password.IsEmpty())
            {
                var pass = mdl.Password.EncryptString();
                var user = _repoUser.GetAll().Where(c => c.IsActive == true && !c.IsDeleted && (c.UserName == mdl.UserCode || c.Email == mdl.UserCode) && c.PasswordHash == pass).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
            }

            return null;
        }
        #endregion
        #region تغيير كلمة المرور
        public ResultViewModel ChangeOldPasswordWeb(ChangePasswordDTO mdl)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Status = false, Message = AppConstants.Messages.SavedFailed };
            if (mdl != null)
            {
                var currentUser = _repoUser.GetById(_repoUser.UserId);
                if (currentUser != null)
                {
                    var hashPass = mdl.OldPassword.EncryptString();
                    if (hashPass == currentUser.PasswordHash)
                    {
                        var hashNewPass = (mdl.NewPassword).EncryptString();
                        if (hashNewPass != null)
                        {
                            currentUser.PasswordHash = hashNewPass;
                            if (_repoUser.Update(currentUser))
                            {
                                resultViewModel.Status = true;
                                resultViewModel.Message = AppConstants.Messages.SavedSuccess;
                            }
                        }
                    }
                }
            }
            return resultViewModel;
        }

        #endregion

        #endregion
        #endregion
        #region GetUsers
        public IEnumerable<User> GetUsers(Guid[] reguonIds) => _repoUser.GetAllAsNoTracking().Include(p => p.UserRegions).Where(p => p.IsActive && !p.IsDeleted && p.UserRegions.Any(r => r.UserId == p.ID));
        public IEnumerable<User> GetUsers(Guid? regionId)
        {
            if (regionId.HasValue)
            {
                return _repoUser.GetAllAsNoTracking().Include(p => p.UserRegions).Where(p => p.IsActive && !p.IsDeleted && p.UserRegions.Any(r => r.UserId == p.ID&&r.RegionId==regionId));

            }
            return Enumerable.Empty<User>();
        }
        public IEnumerable<User> GetUsersNotDistributed(Guid? regionId)
        {
            if (regionId.HasValue)
            {
                return _repoUser.GetAllAsNoTracking().Include(p => p.UserRegions).Include(p=>p.ManualDistributions)
                    .Where(p => p.IsActive && !p.IsDeleted 
                    && p.UserRegions.Any(r =>  r.RegionId == regionId)
                    &&p.ManualDistributions.Count()==0

                    );

            }
            return Enumerable.Empty<User>();
        }
        #endregion

        #region UserPermissions
        public IEnumerable<UserPermissionsDTO> GetUserPermissions(Guid id)
        {
            IEnumerable<UserPermissionsDTO> data = _repoPage.GetAll().Include(p => p.Area).Include(p => p.ActionsPages).Where(p => p.IsActive && !p.IsDeleted).OrderBy(p=>p.Area.OrderNo).OrderBy(p=>p.OrderNo).Select(p => new UserPermissionsDTO()
            {
                PageId = p.ID,
                PageName = p.Text,
                AddPermission = _repoUserPermission.GetAll().Include(p => p.ActionsPage).Where(u => u.ActionsPage.ActionName == ActionEnum.Add && u.UserTypeId == id && u.ActionsPage.PageId == p.ID).Any(),
                ShowPermission = _repoUserPermission.GetAll().Include(p => p.ActionsPage).Where(u => u.ActionsPage.ActionName == ActionEnum.Show && u.UserTypeId == id && u.ActionsPage.PageId == p.ID).Any(),
                DeletePermission = _repoUserPermission.GetAll().Include(p => p.ActionsPage).Where(u => u.ActionsPage.ActionName == ActionEnum.Delete && u.UserTypeId == id && u.ActionsPage.PageId == p.ID).Any(),
                EditPermission = _repoUserPermission.GetAll().Include(p => p.ActionsPage).Where(u => u.ActionsPage.ActionName == ActionEnum.Edit && u.UserTypeId == id && u.ActionsPage.PageId == p.ID).Any(),
                ApprovedPermission = _repoUserPermission.GetAll().Include(p => p.ActionsPage).Where(u => u.ActionsPage.ActionName == ActionEnum.Approved && u.UserTypeId == id && u.ActionsPage.PageId == p.ID).Any(),
                RejectPermission = _repoUserPermission.GetAll().Include(p => p.ActionsPage).Where(u => u.ActionsPage.ActionName == ActionEnum.Reject && u.UserTypeId == id && u.ActionsPage.PageId == p.ID).Any(),

                FinalApprovedPermission = _repoUserPermission.GetAll().Include(p => p.ActionsPage).Where(u => u.ActionsPage.ActionName == ActionEnum.FinalApproved && u.UserTypeId == id && u.ActionsPage.PageId == p.ID).Any(),
                RegionsToUserPermission = _repoUserPermission.GetAll().Include(p => p.ActionsPage).Where(u => u.ActionsPage.ActionName == ActionEnum.RegionsToUser && u.UserTypeId == id && u.ActionsPage.PageId == p.ID).Any(),

            });


            return data;
        }
        public ResultViewModel SaveUserPermission(Guid userTypeId, IEnumerable<UserPermissionsSaveDTO> userPermissionsDTOs)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };
            foreach (var item in userPermissionsDTOs)
            {
                if (item.PageId==Guid.Parse("16ead915-1e11-4b9a-ac79-e23625440bd2"))
                {

                }
                var ActionData = _repoActionsPage.GetAll().Where(p => p.PageId == item.PageId && p.ActionName == Enum.Parse<ActionEnum>(item.ActionName)).FirstOrDefault();
                if (ActionData == null)
                {
                    continue;
                }
                var ActionId = ActionData.ID;
                var data = _repoUserPermission.GetAll().Where(p => p.PageActionId == ActionId && p.UserTypeId == userTypeId).FirstOrDefault();
                if (data == null)
                {
                    if (item.HasPermission)
                    {
                        var tbl = new UserPermission();
                        tbl.PageActionId = ActionId;
                        tbl.UserTypeId = userTypeId;
                        _repoUserPermission.InsertWithoutSaveChange(tbl);
                    }
                }
                else
                {
                    if (item.HasPermission)
                    {

                        _repoUserPermission.UpdateWithoutSaveChange(data);
                    }
                    else
                    {
                        _repoUserPermission.DeleteWithoutSaveChange(data);
                    }
                }
            }
            if (_repoUserPermission.SaveChange())
            {
                resultViewModel.Status = true;
                resultViewModel.Message = AppConstants.Messages.SavedSuccess;
            }
            return resultViewModel;
        }

        #endregion
    }
}
