using AutoMapper;
using GasStationRatingSystem.Common;
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
   public class ManualDistributionBll
    {
        #region Fields
        private const string _spManualDistributions = "Setting.[spManualDistribution]";

        private readonly IRepository<ManualDistribution> _repoManualDistribution;
        private readonly IRepository<VisitInfo> _repoVisitInfo;


        public ManualDistributionBll(IRepository<ManualDistribution> repoManualDistribution, IRepository<VisitInfo> repoVisitInfo)
        {
            _repoManualDistribution = repoManualDistribution;
            _repoVisitInfo = repoVisitInfo;
        }

        #endregion
        #region Get
        

        public ManualDistribution GetById(Guid id)
        {
           
            return _repoManualDistribution.GetAllAsNoTracking().Where(p=>p.ID==id).FirstOrDefault();
        }
        
        #endregion


        #region Web
        public ResultViewModel Save(List< ManualDistributionDTO> ManualDistributionListDTO,bool fromApi=false)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };
            foreach (var ManualDistributionDTO in ManualDistributionListDTO)
            {
                var config = new MapperConfiguration(p => p.CreateMap<ManualDistributionDTO, ManualDistribution>());
                var mapper = new Mapper(config);
                var ManualDistribution = mapper.Map<ManualDistribution>(ManualDistributionDTO);



                var tbl = _repoManualDistribution.GetById(ManualDistributionDTO.ID);
                if (tbl == null)
                {
                    if (fromApi==false&&_repoManualDistribution.GetAll().Where(p => p.StationId == ManualDistributionDTO.StationId && p.IsActive && !p.IsDeleted).FirstOrDefault() != null)
                    {
                        resultViewModel.Message = "المحطة مربوطة من قبل";
                          return resultViewModel;

                    }
                    else
                    {


                        ManualDistribution.ID = Guid.NewGuid();

                        if (_repoManualDistribution.Insert(ManualDistribution))
                        {
                            resultViewModel.Status = true;
                            resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                        }
                    }
                }
                else
                {
                    if (fromApi == false&&_repoManualDistribution.GetAll().Where(p => p.ID != ManualDistributionDTO.ID && p.StationId == ManualDistributionDTO.StationId && p.IsActive && !p.IsDeleted).FirstOrDefault() != null)
                    {
                        resultViewModel.Message = "المحطة مربوطة من قبل";
                        return resultViewModel;

                    }
                    else
                    {
                        tbl.StationId = ManualDistribution.StationId;
                        tbl.IsActive = ManualDistribution.IsActive;
                        tbl.UserId = ManualDistribution.UserId;
                        if (_repoManualDistribution.Update(tbl))
                        {
                            resultViewModel.Status = true;
                            resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                        }
                    }
                }
            }

            resultViewModel.Status = true;
            resultViewModel.Message = AppConstants.Messages.SavedSuccess;



            return resultViewModel;
        }
        public ResultViewModel Delete(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoManualDistribution.GetById(id);
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoManualDistribution.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }
        public ResultViewModel ChangeStatus(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoManualDistribution.GetById(id);
            tbl.IsActive = !tbl.IsActive;
            var IsSuceess = _repoManualDistribution.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.ChangedStatusSuccess : AppConstants.Messages.ChangedStatusFailed;


            return resultViewModel;
        }

        #region LoadData
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoManualDistribution.ExecuteStoredProcedure<ManualDistributionDTO>
                (_spManualDistributions, mdl?.ToSqlParameter(_repoManualDistribution.UserId), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
       
        #endregion
    
    }
}
