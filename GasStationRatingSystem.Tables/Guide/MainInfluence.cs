using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using GasStationRatingSystem.Common;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(MainInfluence)+"s",Schema =AppConstants.Areas.Guide)]
    public class MainInfluence:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<MainInfluenceQuestion> MainInfluenceQuestions { get; set; }
    }
    [Table(nameof(MainInfluenceQuestion) + "s", Schema = AppConstants.Areas.Guide)]
    public class MainInfluenceQuestion : BaseEntity
    {
        [ForeignKey(nameof(MainInfluence))]
        public Guid MainInfluenceId { get; set; }
        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public virtual MainInfluence MainInfluence { get; set; }
        public virtual Question Question { get; set; }

    }

}
