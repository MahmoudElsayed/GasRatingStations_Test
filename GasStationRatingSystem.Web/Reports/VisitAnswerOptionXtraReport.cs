using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using GasStationRatingSystem.DTO;

namespace GasStationRatingSystem.Web.Reports
{
    public partial class VisitAnswerOptionXtraReport
    {
        public VisitAnswerOptionXtraReport(Guid VisitId,string QuestionIds)
        {
            InitializeComponent();
            if (VisitId != Guid.Empty)
            {
                //var fromDate = new QueryParameter();
                //fromDate.Name = "@FromDate";
                //fromDate.Type = typeof(DateTime);
                //if (!string.IsNullOrEmpty(mdl.FromDate))
                //    fromDate.Value = mdl.FromDate;
                //else
                //    fromDate.Value = DBNull.Value;
                //this.sqlDataSource1.Queries[0].Parameters.Add(fromDate);
                ////


                var QuestionIdPrameter = new QueryParameter();
                QuestionIdPrameter.Name = "@QuestionIds";
                if (!string.IsNullOrEmpty(QuestionIds))

                    QuestionIdPrameter.Value = QuestionIds;
                else
                    QuestionIdPrameter.Value = DBNull.Value;
                this.sqlDataSource1.Queries[0].Parameters.Add(QuestionIdPrameter);




                this.sqlDataSource1.Queries[0].Parameters.FirstOrDefault(p => p.Name == "@VisitId").Value = VisitId;
            }
        }
    }
}
