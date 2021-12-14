using GasStationRatingSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class DashboardChartDTO
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = AppConstants.Messages.RequiredMessage)]
        public string Title { get; set; }
        [NotMapped]
        [Required(ErrorMessage =AppConstants.Messages.RequiredMessage)]
        public string[] QuestionsIdsArray { get; set; }

        public string QuestionsIds =>  string.Join(",", QuestionsIdsArray);


        public string Color { get; set; }
    }
    public class DashboardChartTableDTO
    {
        public int TotalCount { get; set; }
        public Guid ID { get; set; }
        public string AddedDate { get; set; }

        public string Title { get; set; }
        public string QuestionTexts { get; set; }
        public string Color { get; set; }
    }
}
