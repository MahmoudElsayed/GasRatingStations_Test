using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
   public class GasStationBll
    {
        #region Fields
        private const string _spGasStations = "Guide.[spGasStations]";

        private readonly IRepository<GasStation> _repoGasStation;

        private readonly ManualDistributionBll _manualDistributionBll;

        public GasStationBll(IRepository<GasStation> repoGasStation, ManualDistributionBll manualDistributionBll)
      
        {
            _repoGasStation = repoGasStation;
            _manualDistributionBll = manualDistributionBll;
        }

        #endregion
        #region Get
        public IQueryable<GasStation> GetAll() => _repoGasStation.GetAllAsNoTracking();
        public ResultDTO Get()
        {
            ResultDTO result = new ResultDTO();
            var data = _repoGasStation.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted).Select(p=>new { 
            Id=p.ID, Name=p.NameInLicense.TrimEnd()
                ,Latitude=p.Lat
                ,Longitude=p.Lng
            });
            result.data = new {Stations=data };
            return result;
        }
        public ResultDTO GetStaions()
        {
            ResultDTO result = new ResultDTO();
            var _visitBll = _repoGasStation.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(VisitBll)) as VisitBll;

            var _visitApprovalBll =_repoGasStation.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(VisitApprovalBll)) as VisitApprovalBll;
            var data = _repoGasStation.GetAllAsNoTracking().Include(p => p.ManualDistributions)
                .Include(p=>p.VisitInfo).ThenInclude(p=>p.VisitApprovals)
                .Where(p => p.ManualDistributions.Any(m => (m.UserId == _repoGasStation.UserId&&(m.IsClosed==false&&m.IsVisited==false
                  && (p.VisitInfo.Count(p=>p.IsClosed==false)==0))
                     //|| p.VisitInfo.Any(p1 => p1.IsClosed == false&&p1.FromVisitApproval && p1.AddedBy == _repoGasStation.UserId)
                  ))).Select(p => new {
                      Id = p.ID,
                      Name = p.Name.TrimEnd(),
                      Latitude = p.Lat,
                      Longitude = p.Lng,
                      IsReturned = _visitBll.IsStationRejected(p.ID),
                      Notes = _visitApprovalBll.GetStationRejectedComment(p.ID)
                  }); 



            //var data = _repoManualDistribution.GetAllAsNoTracking().Where(p => p.IsActive && !p.IsDeleted && p.UserId == _repoManualDistribution.UserId
            //&& _repoVisitInfo.GetAllAsNoTracking().Count(s => s.StationId == p.StationId) == 0).Include(p => p.GasStation).Select(p => new {
            //    Id = p.StationId,
            //    Name = p.GasStation.Name.TrimEnd()
            //            ,
            //    Latitude = p.GasStation.Lat
            //            ,
            //    Longitude = p.GasStation.Lng

            //});
            result.data = new { Stations = data };
            return result;
        }
        public ResultDTO GetRejectedStaions()
        {
            ResultDTO result = new ResultDTO();
            var _visitBll = _repoGasStation.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(VisitBll)) as VisitBll;
            var _visitApprovalBll = _repoGasStation.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(VisitApprovalBll)) as VisitApprovalBll;
            var data = _repoGasStation.GetAllAsNoTracking()
                .Include(p => p.VisitInfo).ThenInclude(p => p.VisitApprovals)
                .Where(p => p.VisitInfo.Any(p1 => p1.IsClosed==false&&p1.FromVisitApproval&&p1.AddedBy==_repoGasStation.UserId)

                  ).Select(p => new {
                      Id = p.ID,
                      Name = p.Name.TrimEnd(),
                      Latitude = p.Lat,
                      Longitude = p.Lng,
                      IsReturned = true,
                      VisitId=_visitBll.GetAll().Where(v=>v.StationId==p.ID&&v.IsClosed==false&&v.FromVisitApproval).OrderBy(p=>p.CreatedDate).LastOrDefault().ID,
                      VisitTime = _visitBll.GetAll().Where(p1=>p1.ID== _visitBll.GetAll().Where(v => v.StationId == p.ID && v.IsClosed == false && v.FromVisitApproval).OrderBy(p => p.CreatedDate).LastOrDefault().ID).FirstOrDefault().VisitTime.ToString("dd/MM/yyyy hh:mm:ss tt"),
                      Notes = _visitApprovalBll.GetStationRejectedComment(p.ID)
                  });



         
            result.data = new { Stations = data };
            return result;
        }

        public GasStation GetById(Guid id)
        {

            return _repoGasStation.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
        }
        public GasStation GetByIdWithAddtionalData(Guid id)
        {

            return _repoGasStation.GetAllAsNoTracking().Where(p => p.ID == id).Include(p=>p.District).ThenInclude(p=>p.City).ThenInclude(p=>p.Region).ThenInclude(p=>p.Branch).FirstOrDefault();
        }
        public IEnumerable<GasStation> GetStaions(Guid districtId)
        {
            var _userRegions = _repoGasStation.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<UserRegion>)) as IRepository<UserRegion>;


            return _repoGasStation.GetAllAsNoTracking().Include(p => p.District).ThenInclude(p => p.City).Where(p => p.DistrictId == districtId && _userRegions.GetAllAsNoTracking()
                .Any(r => r.UserId == _repoGasStation.UserId && r.RegionId == p.District.City.RegionId));
        }
        public IEnumerable<SelectListDTO> GetStaionsByDistricts(Guid ?[]districtIds)
        {

            return _repoGasStation.GetAllAsNoTracking().Include(p=>p.District).Where(p => districtIds.Contains(p.DistrictId)&&!p.IsDeleted&&p.IsActive).Select(p=>new SelectListDTO() { Value=p.ID,Text=$"{p.Name}-{p.District.Name}" });
        }
        public IEnumerable<GasStation> GetStaionsNotDistributed(Guid districtId,Guid?stationId)
        {
            var _userRegions = _repoGasStation.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<UserRegion>)) as IRepository<UserRegion>;


            var data= _repoGasStation.GetAllAsNoTracking().Include(p => p.District).ThenInclude(p => p.City).Include(p=>p.ManualDistributions)
                .Where(p => (p.DistrictId == districtId &&(stationId.HasValue?p.ID==stationId: p.ManualDistributions.Count(m=>!m.IsDeleted)==0)&& _userRegions.GetAllAsNoTracking()
                .Any(r => r.UserId == _repoGasStation.UserId && r.RegionId == p.District.City.RegionId)));
            return data;
        }
        #endregion
        public ResultViewModel Save(GasStationDTO GasStationDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoGasStation.GetAllAsNoTracking().Where(p => p.ID == GasStationDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoGasStation.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Name.Trim().ToLower() == GasStationDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var config = new MapperConfiguration(p => p.CreateMap<GasStationDTO, GasStation>());
                var mapper = new Mapper(config);
                var tbl = mapper.Map<GasStation>(GasStationDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                if (_repoGasStation.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoGasStation.GetAllAsNoTracking().Where(p => p.Name.Trim().ToLower() == GasStationDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var config = new MapperConfiguration(p => p.CreateMap<GasStationDTO, GasStation>());
                var mapper = new Mapper(config);
                var tbl = mapper.Map<GasStation>(GasStationDTO);

                if (_repoGasStation.Insert(tbl))
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
            var tbl = _repoGasStation.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoGasStation.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }




        #region LoadData
        public DataTableResponse LoadData(DataTableRequest mdl,int gasStationVisitStatus)
        {
            var data = _repoGasStation.ExecuteStoredProcedure<GasStationDTO>
                (_spGasStations, mdl?.ToSqlParameter(_repoGasStation.UserId,null,null,new SqlParameter("@GasStationVisitStatus",gasStationVisitStatus)), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
      
        #endregion
    }
}
