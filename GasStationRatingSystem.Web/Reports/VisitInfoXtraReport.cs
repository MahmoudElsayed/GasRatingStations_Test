using System;
using System.Linq;
using DevExpress.XtraReports.UI;
using GasStationRatingSystem.DTO;

namespace GasStationRatingSystem.Web.Reports
{
    public partial class VisitInfoXtraReport
    {
        public VisitInfoXtraReport(RatingsReportDTO mdl)
        {
            InitializeComponent();
            this.sqlDataSource1.Queries[0].Parameters.Where(p => p.Name == "@FromDate").FirstOrDefault().Value = !string.IsNullOrEmpty(mdl.FromDate) ? DateTime.ParseExact(mdl.FromDate, "yyyy-MM-dd", null).ToString("MM-dd-yyyy") : null;
            sqlDataSource1.Queries[0].Parameters.Where(p => p.Name == "@ToDate").FirstOrDefault().Value = !string.IsNullOrEmpty(mdl.ToDate) ? DateTime.ParseExact(mdl.ToDate, "yyyy-MM-dd", null).ToString("MM-dd-yyyy") : null;


            sqlDataSource1.Queries[0].Parameters.Where(p => p.Name == "@BranchIds").FirstOrDefault().Value = mdl.BranchIds;
            sqlDataSource1.Queries[0].Parameters.Where(p => p.Name == "@RegionIds").FirstOrDefault().Value = mdl.RegionIds;
            sqlDataSource1.Queries[0].Parameters.Where(p => p.Name == "@CityIds").FirstOrDefault().Value = mdl.CityIds;
            sqlDataSource1.Queries[0].Parameters.Where(p => p.Name == "@DistrictIds").FirstOrDefault().Value = mdl.DistrictIds;
            sqlDataSource1.Queries[0].Parameters.Where(p => p.Name == "@UserIds").FirstOrDefault().Value = mdl.UserIds;
            //this.Parameters.Add(new DevExpress.XtraReports.Parameters.Parameter() { Name = "mdl", Value = mdl, Type = typeof(RatingsReportDTO) });
            this.Parameters["QuestionIds"].Value = mdl.QuestionIds;
        }

        private void VisitInfoXtraReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
         
        }
        public class GetStationDTO
        {
            public Guid StationId { get; set; }
            public Guid VisitId { get; set; }
            public string StationName { get; set; }

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (Guid.TryParse(this.GetCurrentColumnValue("VisitId")?.ToString() ?? "", out Guid VisitId))
                {

                    VisitAnswerOptionXtraReport OrdersProductClientXtraReport = new VisitAnswerOptionXtraReport(VisitId, this.Parameters["QuestionIds"].Value.ToString());
                    subreport1.ReportSource = OrdersProductClientXtraReport;

                }
            }
            catch
            { }
        }
    }
}
