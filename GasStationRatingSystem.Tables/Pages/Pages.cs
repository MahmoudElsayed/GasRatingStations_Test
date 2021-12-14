using GasStationRatingSystem.Common;
using GasStationRatingSystem.Common.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{


    [Table(nameof(Area) + "s", Schema = AppConstants.Areas.Page)]

    public class Area:BaseEntity
    {
        public string Text { get; set; }
        public int? OrderNo { get; set; }
        public string IconName { get; set; }
        public string Name { get; set; }
        public ICollection<Page> Pages { get; set; }

    }
    [Table(nameof(Page) + "s", Schema = AppConstants.Areas.Page)]

    public class Page : BaseEntity
    {
        public string Text { get; set; }
        public int? OrderNo { get; set; }
        public string IconName { get; set; }
        public string ControllerName { get; set; }
        [ForeignKey(nameof(Area))]
        public Guid? AreaId { get; set; }
        public virtual Area Area { get; set; }
        public ICollection<ActionsPage>  ActionsPages { get; set; }

    }


    [Table(nameof(ActionsPage) + "s", Schema = AppConstants.Areas.Page)]

    public class ActionsPage : BaseEntity
    {
        public string Text { get; set; }

        public ActionEnum  ActionName { get; set; }
        [ForeignKey(nameof(Page))]
        public Guid? PageId { get; set; }
        public virtual Page Page { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }


    }
}
