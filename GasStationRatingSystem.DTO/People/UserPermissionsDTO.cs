using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
  public  class UserPermissionsDTO
    {
        public string PageName { get; set; }
        public string ActionText { get; set; }
        public string ActionName{ get; set; }
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
        public bool AddPermission { get; set; } = false;
        public bool EditPermission { get; set; } = false;
        public bool DeletePermission { get; set; } = false;
        public bool ShowPermission { get; set; } = false;
        public bool ApprovedPermission { get; set; } = false;
        public bool FinalApprovedPermission { get; set; } = false;
        public bool RejectPermission { get; set; } = false;
        public bool RegionsToUserPermission { get; set; } = false;



    }
    public class UserPermissionsSaveDTO
    {
        public string PageName { get; set; }
        public string ActionName { get; set; }
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
        public bool HasPermission { get; set; } = false;

    }
}
