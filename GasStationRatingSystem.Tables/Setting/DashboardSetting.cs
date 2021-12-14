using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(DashboardChart)+"s", Schema = AppConstants.Areas.Setting)]
    public class DashboardChart : BaseEntity
    {
        public string Title { get; set; }
        public string QuestionsIds { get; set; }
        public string Color { get; set; }
    }
   
}
