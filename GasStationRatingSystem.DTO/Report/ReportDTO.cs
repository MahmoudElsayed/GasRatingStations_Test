using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
   public class RatingsReportDTO{
        public Guid? VisitId { get; set; } = Guid.Empty;
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string UserIds { get; set; }
        public string QuestionIds { get; set; }
        public string BranchIds { get; set; }
        public string RegionIds { get; set; }
        public string CityIds { get; set; }
        public string DistrictIds { get; set; }
        public string StationIds { get; set; }


    }
    public class RatingsResultReportDTO
    {
        public string StationName { get; set; }
        public Guid VisitId { get; set; }
        public Guid StationId { get; set; }

    }
}
