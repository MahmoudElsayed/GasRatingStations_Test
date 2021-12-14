
using AutoMapper;
using GasStationRatingSystem;
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
    public class BranchBll
    {
        #region Fields
        private const string _spBranchs = "Guide.[spBranches]";

        private readonly IRepository<Branch> _repoBranch;
        private readonly IMapper _mapper;
        public BranchBll(IRepository<Branch> repoBranch, IMapper mapper)
        {
            _repoBranch = repoBranch;
            _mapper = mapper;
        }

        #endregion
        #region Get


        public Branch GetById(Guid id)
        {

            return _repoBranch.GetAllAsNoTracking().Where(p => p.ID == id).Select(p=>new Branch() { ID=p.ID, Name=p.Name } ).FirstOrDefault();
        }
        public IQueryable<SelectListDTO> GetSelect()
        {
            var _userRegions = _repoBranch.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<UserRegion>)) as IRepository<UserRegion>;

            var data = _userRegions.GetAllAsNoTracking().Where(p => p.UserId == _repoBranch.UserId).Include(p => p.Region).ThenInclude(p => p.Branch)
                 .Select(p => new SelectListDTO()
                 {
                     Value = p.Region.BranchId,
                     Text = p.Region.Branch.Name
                 });


            return data.Distinct();
            //return _repoBranch.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive)
                
            //    .Select(p => new SelectListDTO()
            //{
            //    Value = p.ID,
            //    Text = p.Name
            //});
        }
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoBranch.ExecuteStoredProcedure<BranchTableDTO>
                (_spBranchs, mdl?.ToSqlParameter(), CommandType.StoredProcedure); 

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        public ResultViewModel Save(BranchDTO BranchDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoBranch.GetAllAsNoTracking().Where(p => p.ID == BranchDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoBranch.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Name.Trim().ToLower() == BranchDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var tbl = _mapper.Map<Branch>(BranchDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                if (_repoBranch.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoBranch.GetAllAsNoTracking().Where(p => p.Name.Trim().ToLower() == BranchDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var tbl = _mapper.Map<Branch>(BranchDTO);

                if (_repoBranch.Insert(tbl))
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
            var tbl = _repoBranch.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoBranch.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }

    }
}
