
using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.DTO.Guide;
using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
namespace GasStationRatingSystem.BLL
{
    public class RegionBll
    {
        #region Fields
        private const string _spRegions = "Guide.[spRegions]";

        private readonly IRepository<Region> _repoRegion;
        private readonly IMapper _mapper;
        public RegionBll(IRepository<Region> repoRegion, IMapper mapper)
        {
            _repoRegion = repoRegion;
            _mapper = mapper;
        }

        #endregion
        #region Get


        public Region GetById(Guid id)
        {

            return _repoRegion.GetAllAsNoTracking().Where(p => p.ID == id).Select(p => new Region() { ID = p.ID, Name = p.Name, BranchId=p.BranchId }).FirstOrDefault();
        }
        public IQueryable<SelectListDTO> GetSelect(Guid ? branchId)
        {

            return _repoRegion.GetAllAsNoTracking().Include(p=>p.UserRegions).Where(p =>p.UserRegions.Any(r=>r.UserId==_repoRegion.UserId&&r.RegionId==p.ID)&& !p.IsDeleted && p.IsActive && (/*!branchId.HasValue ||*/ p.BranchId == branchId)).Select(p => new SelectListDTO()
            {
                Value = p.ID,
                Text = p.Name
            }).Distinct();
        }
        public IQueryable<SelectListDTO> GetSelect(Guid[] branchIds = null)
        {
            return _repoRegion.GetAllAsNoTracking().Include(p => p.UserRegions).Where(p => p.UserRegions.Any(r => r.UserId == _repoRegion.UserId && r.RegionId == p.ID) && !p.IsDeleted && p.IsActive && (branchIds == null || branchIds.Contains( p.BranchId))).Select(p => new SelectListDTO()
            {
                Value = p.ID,
                Text = p.Name
            }).Distinct();
        }
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoRegion.ExecuteStoredProcedure<RegionTableDTO>
                (_spRegions, mdl?.ToSqlParameter(), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        public ResultViewModel Save(RegionDTO RegionDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoRegion.GetAllAsNoTracking().Where(p => p.ID == RegionDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoRegion.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Name.Trim().ToLower() == RegionDTO.Name.Trim().ToLower()&&p.BranchId== RegionDTO.BranchId).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var tbl = _mapper.Map<Region>(RegionDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                if (_repoRegion.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoRegion.GetAllAsNoTracking().Where(p => p.Name.Trim().ToLower() == RegionDTO.Name.Trim().ToLower() && p.BranchId == RegionDTO.BranchId).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var tbl = _mapper.Map<Region>(RegionDTO);

                if (_repoRegion.Insert(tbl))
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
            var tbl = _repoRegion.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoRegion.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }

    }
}
