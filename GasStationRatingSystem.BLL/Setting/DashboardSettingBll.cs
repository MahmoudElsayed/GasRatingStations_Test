
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
    public class DashboardChartBll
    {
        #region Fields
        private const string _spDashboardCharts = "Setting.[spDashboardCharts]";

        private readonly IRepository<DashboardChart> _repoDashboardChart;
        private readonly IMapper _mapper;
        public DashboardChartBll(IRepository<DashboardChart> repoDashboardChart, IMapper mapper)
        {
            _repoDashboardChart = repoDashboardChart;
            _mapper = mapper;
        }

        #endregion
        #region Get


        public DashboardChart GetById(Guid id)
        {

            return _repoDashboardChart.GetAllAsNoTracking().Where(p => p.ID == id).Select(p => new DashboardChart() { ID = p.ID, Title = p.Title, QuestionsIds=p.QuestionsIds }).FirstOrDefault();
        }
        
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoDashboardChart.ExecuteStoredProcedure<DashboardChartTableDTO>
                (_spDashboardCharts, mdl?.ToSqlParameter(), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        public ResultViewModel Save(DashboardChartDTO DashboardChartDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoDashboardChart.GetAllAsNoTracking().Where(p => p.ID == DashboardChartDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoDashboardChart.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Title.Trim().ToLower() == DashboardChartDTO.Title.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var tbl = _mapper.Map<DashboardChart>(DashboardChartDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                if (_repoDashboardChart.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoDashboardChart.GetAllAsNoTracking().Where(p => p.Title.Trim().ToLower() == DashboardChartDTO.Title.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var tbl = _mapper.Map<DashboardChart>(DashboardChartDTO);

                if (_repoDashboardChart.Insert(tbl))
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
            var tbl = _repoDashboardChart.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoDashboardChart.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }

    }
}
