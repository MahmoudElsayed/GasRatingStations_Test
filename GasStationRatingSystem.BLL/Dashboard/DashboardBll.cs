using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
   public class DashboardBll
    {


        #region Fields
        private readonly IRepository<VisitInfo> _repoVisitInfo;
        private const string _spGetTotals= "Dashboard.GetTotals";
        private const string _spGetCitiesSations = "Dashboard.GetCitiesSations";
        private const string _spGetDashboardCharts = "Dashboard.GetDashboardCharts";


        public DashboardBll(IRepository<VisitInfo> repoVisitInfo)
        {
            _repoVisitInfo = repoVisitInfo;
        }
        #endregion
        #region Helpers
        public DashboardGetTotalsDTO GetTotals(DateTime? dateFrom, DateTime? dateTo)
        {
            return _repoVisitInfo.ExecuteStoredProcedure<DashboardGetTotalsDTO>(_spGetTotals, new SqlParameter[] {
                
                new SqlParameter("@UserId",_repoVisitInfo.UserId)
              ,new SqlParameter("@DateFrom", dateFrom)
            ,new SqlParameter("@DateTo", dateTo)

            })?.FirstOrDefault() ?? new DashboardGetTotalsDTO();
        }
        public  List<DashboardGetTotalsDTO> GetCitiesSations(DateTime? dateFrom,DateTime? dateTo)
        {
            return _repoVisitInfo.ExecuteStoredProcedure<DashboardGetTotalsDTO>(_spGetCitiesSations, new SqlParameter[] { new SqlParameter("@UserId", _repoVisitInfo.UserId)
            ,new SqlParameter("@DateFrom", dateFrom)
            ,new SqlParameter("@DateTo", dateTo)

            }).OrderByDescending(p=>p.NotStarted).Where(p=>p.Total>0)?.ToList();
        }
      
        public List<RatingsResultReportDTO> GetVisitInfo(RatingsReportDTO mdl)
        {
            return _repoVisitInfo.ExecuteStoredProcedure<RatingsResultReportDTO>("[Dashboard].[GetVisitInfo]", new SqlParameter[] {

//                @BranchIds nvarchar(max) null =null,
//@RegionIds nvarchar(max) null=null,
//@CityIds nvarchar(max) null =null,
//@DistrictIds nvarchar(max)null =null,
//@UserIds nvarchar(max)null =null,
//@FromDate nvarchar(max)null =null,
//@ToDate nvarchar(max)null =null


                new SqlParameter("@UserIds", mdl.UserIds),
                new SqlParameter("@RegionIds", mdl.RegionIds),
                new SqlParameter("@CityIds", mdl.CityIds),
                new SqlParameter("@DistrictIds", mdl.DistrictIds),
                new SqlParameter("@FromDate", mdl.FromDate),
                new SqlParameter("@ToDate", mdl.ToDate),



            });
        }


        public List<DashboardChartsDTO> GetDashboardCharts(DateTime? dateFrom, DateTime? dateTo)
        {
            return _repoVisitInfo.ExecuteStoredProcedure<DashboardChartsDTO>(_spGetDashboardCharts, 
                
                new SqlParameter[] {    new SqlParameter("@DateFrom", dateFrom)
            ,new SqlParameter("@DateTo", dateTo)
}
                , System.Data.CommandType.StoredProcedure).Where(p=>p.Total!=0)?.ToList();
        }
        #endregion
    }
}
