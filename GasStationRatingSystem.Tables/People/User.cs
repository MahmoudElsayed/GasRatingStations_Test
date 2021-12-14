using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(User) + "s", Schema = AppConstants.Areas.People)]

    public partial class User:BaseEntity
    {
        [Required, StringLength(100)]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;
        /// <summary>
        /// هل قام باستخدام الباسورد الافتراضي ام لا
        /// </summary>
        public bool UseDefaultPassword { get; set; } = true;

        #region Reset

        public DateTime? ResetPasswordDate { get; set; }

        public string CodeOfReset { get; set; } = string.Empty;

        #endregion

        /// <summary>
        /// For Password 
        /// </summary>
        public string Salt { get; set; }
        [ForeignKey(nameof(UserType))]
        public Guid? UserTypeId { get; set; }
        /// <summary>
        /// اسم المفتش
        /// </summary>
        public string InspectorName { get; set; }


    }
}
