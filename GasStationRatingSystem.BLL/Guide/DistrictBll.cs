using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.DTO.Guide;
using GasStationRatingSystem.Tables;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GasStationRatingSystem.BLL
{
    public class DistrictBll
    {
        #region Fields
        private const string _spDistricts = "Guide.[spDistricts]";

        private readonly IRepository<District> _repoDistrict;
        private readonly IMapper _mapper;
        public DistrictBll(IRepository<District> repoDistrict, IMapper mapper)
        {
            _repoDistrict = repoDistrict;
            _mapper = mapper;
        }

        #endregion
        #region Get


        public District GetById(Guid id)
        {

            return _repoDistrict.GetAllAsNoTracking().Where(p => p.ID == id).Include(p=>p.City).Select(p => new District() { ID = p.ID, Name = p.Name , CityId=p.CityId }).FirstOrDefault();
        }
        public List<SelectListDTO> GetSelect(Guid? cityId=null)
        {


            var _userRegions = _repoDistrict.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<UserRegion>)) as IRepository<UserRegion>;


            return _repoDistrict.GetAllAsNoTracking().Include(p=>p.City).Where(p => !p.IsDeleted && p.IsActive&&(/*!cityId.HasValue||*/p.CityId==cityId&& _userRegions.GetAllAsNoTracking().Any(p1=>p1.UserId==_userRegions.UserId&&p1.RegionId==p.City.RegionId))).Select(p => new SelectListDTO()
            {
                Value = p.ID,
                Text = p.Name
            })?.Distinct().ToList() ;
        }
        public List<SelectListDTO> GetSelect(Guid?[] cityIds = null)
        {



            var _userRegions = _repoDistrict.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<UserRegion>)) as IRepository<UserRegion>;


            var data= _repoDistrict.GetAllAsNoTracking().Include(p => p.City).Where(p => !p.IsDeleted && p.IsActive && ((cityIds == null || cityIds.Contains(p.CityId)) && _userRegions.GetAllAsNoTracking().Any(p1 => p1.UserId == _userRegions.UserId && p1.RegionId == p.City.RegionId))).Select(p => new SelectListDTO()
            {
                Value = p.ID,
                Text = p.Name
            });
            return data.Distinct()?.ToList();
            //return _repoDistrict.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive && (cityIds==null || cityIds.Contains( p.CityId))).Select(p => new SelectListDTO()
            //{
            //    Value = p.ID,
            //    Text = p.Name
            //});
        }
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoDistrict.ExecuteStoredProcedure<DistrictTableDTO>
                (_spDistricts, mdl?.ToSqlParameter(), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        public ResultViewModel Save(DistrictDTO DistrictDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoDistrict.GetAllAsNoTracking().Where(p => p.ID == DistrictDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoDistrict.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Name.Trim().ToLower() == DistrictDTO.Name.Trim().ToLower() && p.CityId == DistrictDTO.CityId).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var tbl = _mapper.Map<District>(DistrictDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                if (_repoDistrict.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoDistrict.GetAllAsNoTracking().Where(p => p.Name.Trim().ToLower() == DistrictDTO.Name.Trim().ToLower() && p.CityId == DistrictDTO.CityId).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var tbl = _mapper.Map<District>(DistrictDTO);

                if (_repoDistrict.Insert(tbl))
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
            var tbl = _repoDistrict.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoDistrict.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }

    }
}
