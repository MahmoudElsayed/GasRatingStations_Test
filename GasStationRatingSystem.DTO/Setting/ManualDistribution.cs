using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
   public class ManualDistributionDTO
    {
        public int TotalCount { get; set; }
        public string AddedDate { get; set; }
        public Guid ID { get; set; }
        public Guid? UserId { get; set; }
        public Guid? StationId { get; set; }
        public Guid? DistrictId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? BranchId { get; set; }
        public string UserName { get; set; }
        public string StationName { get; set; }
        public bool IsVisited { get; set; } = false;
        public bool FromVisitApproval { get; set; } = false;
        public bool IsClosed { get; set; } = false;

    }
}
