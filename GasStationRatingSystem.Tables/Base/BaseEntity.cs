
using GasStationRatingSystem.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GasStationRatingSystem.Tables
{
    /// <summary>
    /// This The Base Entity Of Some Tables
    /// Contains ID as Guid
    /// Created Date DateTime
    /// Added User Guid , User
    /// Modified User Guid , User
    /// Deleted User Guid , User
    /// IsActive bool => default true
    /// IsDeleted bool => defult false
    /// </summary>
    public class BaseEntity
    {
        public BaseEntity() => ID = Guid.NewGuid();

        /// <summary>
        /// ID Is The Id Of Table 
        /// uniqueidentifier
        /// </summary>
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// Creation Date Of The Item
        /// </summary>
        public DateTime CreatedDate { get; set; } = AppDateTime.Now;

        /// <summary>
        /// This Should Be The Added User Id
        /// </summary>
        [ForeignKey(nameof(CreatedUser))]
        public Guid? AddedBy { get; set; }


        /// <summary>
        /// Modified Date Of The Item
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// This Should Be The Modified User Id
        /// </summary>
        [ForeignKey(nameof(ModifiedUser))]
        public Guid? ModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The Date of Deleted This Item
        /// </summary>
        public DateTime? DeletedDate { get; set; }

        /// <summary>
        /// User Deleted this Item
        /// </summary>
        [ForeignKey(nameof(DeletedUser))]
        public Guid? DeletedBy { get; set; }


        #region Relations

        public User CreatedUser { get; set; }

        public User ModifiedUser { get; set; }

        public User DeletedUser { get; set; }

        #endregion 

    }
}
