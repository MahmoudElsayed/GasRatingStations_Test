using GasStationRatingSystem.Common;
using GasStationRatingSystem.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class UserParameters
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.UsernameRequired))]
        public string Username { get; set; }
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.PasswordRequired))]

        public string Password { get; set; }
    }
    public class UserChangePasswordParameters
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.PasswordRequired))]

        public string NewPassword { get; set; }
    }
    public class UserSendCodeParameters
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.UsernameRequired))]

        public string Username { get; set; }
    }
    public class UserForgetPasswordParameters
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.UsernameRequired))]

        public string Username { get; set; }

        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.PasswordRequired))]

        public string NewPassword { get; set; }
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.ValidationCodeRequired))]

        public string Code { get; set; }
    }
    public class UserResultDTO
    {
        public Guid Id { get; set; }
        public string BearerToken { get; set; }
        public string Username { get; set; }
        public bool UseDefaultPassword { get; set; } = true;
    }
    #region Web
    public class UserDTO
    {
        public int TotalCount { get; set; } = 0;
        public Guid ID { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage), StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;

        public bool IsActive { get; set; }
        public string AddedDate { get; set; }
        public string RegionIdAsString { get; set; }
        public Guid[] RegionIds { get; set; }

        public string RegionNames { get; set; }
        public Guid? UserTypeId { get; set; }

        public string CityName { get; set; }
        public string UserTypeName { get; set; }

        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage), StringLength(100)]

        /// <summary>
        /// اسم المفتش
        /// </summary>
        public string InspectorName { get; set; }

    }
    #endregion
}
