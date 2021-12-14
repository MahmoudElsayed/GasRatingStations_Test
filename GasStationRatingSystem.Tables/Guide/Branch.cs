using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    /// <summary>
    /// الفروع
    /// </summary>
    [Table(nameof(Branch) + "es", Schema = AppConstants.Areas.Guide)]

    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<Region> Regions { get; set; }
    }
    /// <summary>
    /// دليل المناطق
    /// </summary>
    [Table(nameof(Region) + "s", Schema = AppConstants.Areas.Guide)]
    public class Region : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(Branch))]
        public Guid BranchId { get; set; }

        #region Relations
        public virtual Branch Branch { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<UserRegion> UserRegions { get; set; }
        #endregion
    }


    /// <summary>
    /// المدن
    /// </summary>
    [Table("Cities", Schema = AppConstants.Areas.Guide)]

    public class City:BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(Region))]
        public Guid? RegionId { get; set; }

        public Region Region { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
    /// <summary>
    /// الاحياء
    /// </summary>
    [Table( nameof(District)+"s", Schema = AppConstants.Areas.Guide)]

    public class District : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(City))]
        public Guid? CityId { get; set; }
        public virtual City City { get; set; }
        public ICollection<GasStation> GasStations { get; set; }

    }
}
