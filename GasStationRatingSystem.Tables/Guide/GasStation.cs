using GasStationRatingSystem.Common;
using GasStationRatingSystem.Common.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{

    

    [Table(nameof(GasStation) + "s", Schema = AppConstants.Areas.Guide)]
    public class GasStation:BaseEntity
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string NameInLicense { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string PostCode { get; set; }
        public string BuildingNo { get; set; }
        public string AreaName { get; set; }
        public string ElectricityNumber { get; set; }
        public string EnvDescriptionNorth { get; set; }
        public string EnvDescriptionNorthNotes { get; set; }
        public string EnvDescriptionEast { get; set; }
        public string EnvDescriptionEastNotes { get; set; }
        public string EnvDescriptionWest { get; set; }
        public string EnvDescriptionWestNotes { get; set; }
        public string EnvDescriptionSouth { get; set; }
        public string EnvDescriptionSouthNotes { get; set; }
        public string Code { get; set; }
        public string InspectorName { get; set; }
        public string StationClass { get; set; }
        public string StatusText { get; set; }
        public string AuthorityBranch { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string StreetName { get; set; }
        public DateTime? AddedDate { get; set; }
        public int? WorkerCount { get; set; }
        public float? CountOfDailyWorkingHours { get; set; }
        public float? CountOfVentilationTubes { get; set; }
        public float? HeightOfVentilationPipe { get; set; }

        public int Station { get; set; }

        public string LauncherName { get; set; }

        public string PlatePicture { get; set; }
        public string Accreditation { get; set; }
        [ForeignKey(nameof(District))]
        public Guid? DistrictId { get; set; }
        public GasStationVisitStatus? GasStationVisitStatusNo { get; set; }

        public ICollection<GasStationContact> GasStationContacts { get; set; }
        public ICollection<VisitInfo> VisitInfo { get; set; }
        public ICollection<ManualDistribution> ManualDistributions { get; set; }
        public virtual District District { get; set; }


    }
    [Table(nameof(GasStationContact) + "s", Schema = AppConstants.Areas.Guide)]

    public class GasStationContact:BaseEntity
    {
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
        [ForeignKey(nameof(GasStation))]
        public Guid GasStationId { get; set; }
        public virtual GasStation GasStation { get; set; }
    }
    public enum ContactType
    {
        Phone,Email
    }
}
