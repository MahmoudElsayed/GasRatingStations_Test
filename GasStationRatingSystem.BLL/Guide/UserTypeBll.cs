
using AutoMapper;
using GasStationRatingSystem;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.DTO.Guide;
using GasStationRatingSystem.Tables;

using System;
using System.Data;
using System.Linq;

namespace GasStationRatingSystem.BLL
{
    public class UserTypeBll
    {
        #region Fields
        private const string _spUserTypes = "Guide.[spUserTypes]";

        private readonly IRepository<UserType> _repoUserType;
        private readonly IMapper _mapper;
        public UserTypeBll(IRepository<UserType> repoUserType, IMapper mapper)
        {
            _repoUserType = repoUserType;
            _mapper = mapper;
        }

        #endregion
        #region Get


        public UserType GetById(Guid id)
        {

            return _repoUserType.GetAllAsNoTracking().Where(p => p.ID == id).Select(p=>new UserType() { ID=p.ID, Name=p.Name } ).FirstOrDefault();
        }
        public IQueryable<SelectListDTO> GetSelect()
        {
            return _repoUserType.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive).Select(p => new SelectListDTO()
            {
                Value = p.ID,
                Text = p.Name
            });
        }
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoUserType.ExecuteStoredProcedure<UserTypeTableDTO>
                (_spUserTypes, mdl?.ToSqlParameter(), CommandType.StoredProcedure); 

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        public ResultViewModel Save(UserTypeDTO UserTypeDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoUserType.GetAllAsNoTracking().Where(p => p.ID == UserTypeDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoUserType.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Name.Trim().ToLower() == UserTypeDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var tbl = _mapper.Map<UserType>(UserTypeDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                if (_repoUserType.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoUserType.GetAllAsNoTracking().Where(p => p.Name.Trim().ToLower() == UserTypeDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var tbl = _mapper.Map<UserType>(UserTypeDTO);

                if (_repoUserType.Insert(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }





            return resultViewModel;
        }

        public ResultViewModel Delete(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoUserType.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoUserType.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }

    }
}
