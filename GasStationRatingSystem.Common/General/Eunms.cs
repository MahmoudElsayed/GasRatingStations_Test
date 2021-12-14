using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStationRatingSystem.Common.General
{

    public enum ActionEnum
    {
        Add = 1, Edit, Delete, Show, Approved, FinalApproved,
        Reject, RegionsToUser
    }
    /// <summary>
    /// 
    /// </summary>
    public enum GasStationVisitStatus
    {
        /// <summary>
        /// غير موزعة
        /// </summary>
        [Description("غير موزعة")]

        UnDistributed,
        /// <summary>
        /// موزعة و لكن لم تتم الزيارة
        /// </summary>
        [Description("تم توزيعها و لم تتم الزيارة")]

        Distributed,
        /// <summary>
        /// تم زيارتها
        /// </summary>
        [Description("تم زيارتها")]
        Visited
    }
    public enum ApprovalCasesEnum
    {
        NotUsed = -1,
        [Description("تمت الموافقة الاولى")]
        FirstApproval,
        [Description("تمت الموافقة الثانية")]

        SecondApproval,
        [Description("تم الرفض")]

        Rejected,
    }
    public enum StationStatus
    {
        UnderConstruction,
        Closed,
        Opened
    }

}
