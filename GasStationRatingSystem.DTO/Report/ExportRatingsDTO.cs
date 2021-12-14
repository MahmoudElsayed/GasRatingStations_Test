using System.Collections.Generic;

namespace GasStationRatingSystem.DTO
{
    public class ExportReportQuestion
    {
        public string Text { get; set; }
        public string AnswerText { get; set; }
    }

    public class ExportReportResultDTO
    {
        public string Result { get; set; }
    }
    public class QuestionList
    {
        public List<ExportReportQuestion> Question { get; set; }
    }

    public class Datum
    {
        public string StationId { get; set; }
        public string StationCode { get; set; }
        public string InspectorName { get; set; }
        public string StationName { get; set; }
        public string DistrictName { get; set; }
        public string StreetName { get; set; }

        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string BranchName { get; set; }
        public string VisitTime { get; set; }
        public QuestionList QuestionList { get; set; }
    }

    public class ExportRatingsDTO
    {
        public List<Datum> Data { get; set; }
    }


   
}
