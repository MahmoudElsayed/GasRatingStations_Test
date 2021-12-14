using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class LogInDTO
    {
        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage)]
        public string UserCode { get; set; }

        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage)]
        public string Password { get; set; }

    }
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage), Compare(nameof(NewPassword), ErrorMessage = "كلمه المرور غير متطابقة")]
        public string ConfirmNewPassword { get; set; }
    }
}
