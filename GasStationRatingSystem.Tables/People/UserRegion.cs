using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(UserRegion) + "s", Schema = AppConstants.Areas.People)]

    public class UserRegion:BaseEntity

    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(Region))]

        public Guid RegionId { get; set; }
        public virtual User User { get; set; }
        public virtual Region Region { get; set; }

    }
}
