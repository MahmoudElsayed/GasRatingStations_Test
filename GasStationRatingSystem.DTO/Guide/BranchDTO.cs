using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO.Guide
{
    public class BranchDTO
    {
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }

    }
    public class BranchTableDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string AddedDate { get; set; }
        public int TotalCount { get; set; }
    }
}
