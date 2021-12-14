using GasStationRatingSystem.DAL;
using GasStationRatingSystem.Tables;
using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.DTO.Guide;
using GasStationRatingSystem.Tables;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GasStationRatingSystem.BLL
{

   public class CityBll
    {
        #region Fields
        private const string _spCities = "Guide.[spCities]";

        private readonly IRepository<City> _repoCity;
        private readonly IMapper _mapper;
        public CityBll(IRepository<City> repoCity, IMapper mapper)
        {
            _repoCity = repoCity;
            _mapper = mapper;
        }

        #endregion
        #region Get


        public City GetById(Guid id)
        {

            return _repoCity.GetAllAsNoTracking().Where(p => p.ID == id).Select(p => new City() { ID = p.ID, Name = p.Name, RegionId=p.RegionId }).FirstOrDefault();
        }
        public IQueryable<SelectListDTO> GetSelect(Guid?regionId)
        {

            var _userRegions = _repoCity.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<UserRegion>)) as IRepository<UserRegion>;

            var data = _userRegions.GetAllAsNoTracking().Where(p => p.UserId == _repoCity.UserId).Include(p => p.Region).ThenInclude(p => p.Cities)
                .Where(p=>p.Region.Cities.Any(c=>!regionId.HasValue|| c.RegionId==regionId))
                .SelectMany(p=>p.Region.Cities).Select(p => new SelectListDTO()
                {
                    Value = p.ID,
                    Text = p.Name
                });

            return data.Distinct();


            //return _repoCity.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive && (/*!regionId.HasValue ||*/ p.RegionId == regionId)).Select(p => new SelectListDTO()
            //{
            //    Value = p.ID,
            //    Text = p.Name
            //});
        }
        public IQueryable<SelectListDTO> GetSelect(Guid ? [] regionIds = null)
        {
            var _userRegions = _repoCity.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<UserRegion>)) as IRepository<UserRegion>;

            var data = _userRegions.GetAllAsNoTracking().Where(p => p.UserId == _repoCity.UserId).Include(p => p.Region).ThenInclude(p => p.Cities)
                .Where(p => !p.IsDeleted && p.IsActive && (regionIds == null || regionIds.Contains(p.RegionId)))
                .SelectMany(p => p.Region.Cities).Select(p => new SelectListDTO()
                {
                    Value = p.ID,
                    Text = p.Name
                });

            return data.Distinct();
            //return _repoCity.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive && (regionIds==null || regionIds.Contains(p.RegionId))).Select(p => new SelectListDTO()
            //{
            //    Value = p.ID,
            //    Text = p.Name
            //});
        }
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoCity.ExecuteStoredProcedure<CityTableDTO>
                (_spCities, mdl?.ToSqlParameter(), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        public ResultViewModel Save(CityDTO CityDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoCity.GetAllAsNoTracking().Where(p => p.ID == CityDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoCity.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Name.Trim().ToLower() == CityDTO.Name.Trim().ToLower() && p.RegionId == CityDTO.RegionId).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var tbl = _mapper.Map<City>(CityDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                if (_repoCity.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoCity.GetAllAsNoTracking().Where(p => p.Name.Trim().ToLower() == CityDTO.Name.Trim().ToLower() && p.RegionId == CityDTO.RegionId).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var tbl = _mapper.Map<City>(CityDTO);

                if (_repoCity.Insert(tbl))
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
            var tbl = _repoCity.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoCity.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }
    }
}
