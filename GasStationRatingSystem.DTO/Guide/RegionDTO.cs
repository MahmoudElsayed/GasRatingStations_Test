using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO.Guide
{
    public class RegionDTO
    {
        public Guid ID { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public Guid BranchId { get; set; }
    }
    public class RegionTableDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string BranchName { get; set; }
        public string AddedDate { get; set; }

        public int TotalCount { get; set; }
    }
}
