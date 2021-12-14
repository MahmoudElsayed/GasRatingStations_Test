using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ClosedXML.Excel;
using GasStationRatingSystem.Common;

namespace GasStationRatingSystem.BLL
{

    public class ExportRatingsBll
    {
        private readonly IRepository<VisitInfo> _repoVisitInfo;
        private const string _spExportAnswers = "Dashboard.ExportAnswers";
        public ExportRatingsBll(IRepository<VisitInfo> repoVisitInfo)
        {

            _repoVisitInfo = repoVisitInfo;
        }


        public ResultViewModel ExportAnswers(RatingsReportDTO mdl,string path)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            ExportRatingsDTO data = new ExportRatingsDTO();

            var lstParameters = new List<SqlParameter>();
            lstParameters.Add(new SqlParameter("@UserId", _repoVisitInfo.UserId));

            lstParameters.Add(new SqlParameter("@UserIds", mdl.UserIds));
            lstParameters.Add(new SqlParameter("@BranchIds", mdl.BranchIds));
            lstParameters.Add(new SqlParameter("@RegionIds", mdl.RegionIds));
            lstParameters.Add(new SqlParameter("@CityIds", mdl.CityIds));
            lstParameters.Add(new SqlParameter("@DistrictIds", mdl.DistrictIds));
            lstParameters.Add(new SqlParameter("@StationIds", mdl.StationIds));
            lstParameters.Add(new SqlParameter("@FromDate", mdl.FromDate));
            lstParameters.Add(new SqlParameter("@ToDate", mdl.ToDate));

            string result=    _repoVisitInfo.ExecuteStoredProcedure<ExportReportResultDTO>(_spExportAnswers, lstParameters.ToArray()).FirstOrDefault()?.Result??"";



          data=  JsonConvert.DeserializeObject<ExportRatingsDTO>(result);
            if (data != null)
            {


                IXLWorkbook wb = new XLWorkbook();
                wb.RightToLeft = true;
                IXLWorksheet ws = wb.Worksheets.Add("الاجراء التصحيحي");



                ws.FirstRow().Cell(1).Value = "كود المحطة";
                ws.FirstRow().Cell(2).Value = "اسم المحطة";
                ws.FirstRow().Cell(3).Value = "تاريخ ووقت التقييم";
                ws.FirstRow().Cell(4).Value = "الفرع";
                ws.FirstRow().Cell(5).Value = "المنطقة";
                ws.FirstRow().Cell(6).Value = "المدينة";
                ws.FirstRow().Cell(7).Value = "الحي";
                ws.FirstRow().Cell(8).Value = "اسم الشارع";
                ws.FirstRow().Cell(9).Value = "اسم المفتش";

                int cellIndex = 10;
                foreach (var item in data.Data.FirstOrDefault().QuestionList.Question)
                {
                    ws.FirstRow().Cell(cellIndex).Value = item.Text;
                    cellIndex++;
                }


                int rowIndex = 2;
                foreach (var item in data.Data)
                {
                    var currentRow = ws.Row(rowIndex);
                    currentRow.Cell(1).Value = item.StationCode;
                    currentRow.Cell(2).Value = item.StationName;
                    currentRow.Cell(3).Value = item.VisitTime;
                    currentRow.Cell(4).Value = item.BranchName;
                    currentRow.Cell(5).Value = item.RegionName;
                    currentRow.Cell(6).Value = item.CityName;
                    currentRow.Cell(7).Value = item.DistrictName;
                    currentRow.Cell(8).Value = item.StreetName;
                    currentRow.Cell(9).Value = item.InspectorName;
                    cellIndex = 10;
                    foreach (var questionItem in item.QuestionList.Question)
                    {
                        currentRow.Cell(cellIndex).Value = questionItem.AnswerText;
                        cellIndex++;
                    }

                    rowIndex++;


                }
                ws.ColumnWidth = 20;
                wb.SaveAs(path);

                resultViewModel.Status = true;
                resultViewModel.Message = AppConstants.Messages.ExportedSuccess;

            }
            else
            {
                resultViewModel.Status = false;
                resultViewModel.Message = AppConstants.Messages.ExportedFailed;

            }

            return resultViewModel;
        }
    }
}
