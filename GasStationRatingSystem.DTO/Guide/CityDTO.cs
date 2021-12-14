using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO.Guide
{
    public class CityDTO
    {
        public Guid ID { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public Guid RegionId { get; set; }
    }
    public class CityTableDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string RegionName { get; set; }
        public string AddedDate { get; set; }

        public int TotalCount { get; set; }
    }
}
