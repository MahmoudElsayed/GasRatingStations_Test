using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO.Guide
{
    public class DistrictDTO
    {
        public Guid ID { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public Guid? CityId { get; set; }
        public Guid? RegionId { get; set; }

    }
    public class DistrictTableDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }

        public string AddedDate { get; set; }

        public int TotalCount { get; set; }
    }
}
