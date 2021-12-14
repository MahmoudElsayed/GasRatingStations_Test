using GasStationRatingSystem.Common;
using GasStationRatingSystem.Common.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace GasStationRatingSystem.Tables
{
    [Table(nameof(VisitInfo) , Schema = AppConstants.Areas.Visit)]

    public class VisitInfo:BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        [ForeignKey(nameof(GasStation))]

        public Guid StationId { get; set; }
        public DateTime VisitTime { get; set; } = AppDateTime.Now;
        public string VisitNo { get; set; } = AppDateTime.Now.ToString("yyyyMMddHHmm",new CultureInfo("en-US"))+new Random((int)AppDateTime.Now.Ticks).Next(1111, 9999);
        public int? PageIndex { get; set; } = 0;
        public StationStatus? StationStatusNo { get; set; }

        public bool IsClosed { get; set; } = false;
        public bool FromVisitApproval { get; set; } = false;
        public virtual  User User { get; set; }
        public virtual GasStation GasStation { get; set; }
        public virtual ICollection<VisitAnswer> VisitAnswers { get; set; }
        public virtual ICollection<VisitApproval> VisitApprovals { get; set; }
        public virtual VisitQuestionImage VisitQuestionImage { get; set; }
        public virtual VisitSignture VisitSignture { get; set; }
        public virtual VisitNote VisitNote { get; set; }

    }
    [Table(nameof(VisitAnswer)+"s", Schema = AppConstants.Areas.Visit)]

    public class VisitAnswer:BaseEntity
    {
        [ForeignKey(nameof(VisitInfo))]
        public Guid VisitId { get; set; }
        public virtual VisitInfo VisitInfo { get; set; }

        public virtual ICollection<VisitAnswerOption> VisitAnswerOptions { get; set; }
    }
    [Table(nameof(VisitAnswerOption) + "s", Schema = AppConstants.Areas.Visit)]

    public class VisitAnswerOption:BaseEntity
    {
        [ForeignKey(nameof(VisitAnswer))]
        public Guid? VisitAnswerId { get; set; }
        [ForeignKey(nameof(Answer))]

        public Guid? AnswerId { get; set; }
        [ForeignKey(nameof(Question))]

        public Guid? MainItemId { get; set; }
        public Guid? RefId { get; set; }
        public virtual VisitAnswer VisitAnswer { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual Question Question { get; set; }


    }
   
    
    [Table(nameof(VisitQuestionImage) + "s", Schema = AppConstants.Areas.Visit)]
    public class VisitQuestionImage : BaseEntity
    {
        [ForeignKey(nameof(VisitInfo))]
        public Guid VisitId { get; set; }
        public virtual VisitInfo VisitInfo { get; set; }

        public virtual ICollection<VisitQuestionImageDetail> VisitQuestionImageDetails { get; set; }
    }
  
    
    [Table(nameof(VisitQuestionImageDetail) + "s", Schema = AppConstants.Areas.Visit)]
    public class VisitQuestionImageDetail : BaseEntity
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey(nameof(VisitQuestionImage))]
        public Guid VisitQuestionImageId { get; set; }
    
        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public virtual VisitQuestionImage VisitQuestionImage { get; set; }

    }
    
    [Table(nameof(VisitSignture) + "s", Schema = AppConstants.Areas.Visit)]
    public class VisitSignture : BaseEntity
    {
        [ForeignKey(nameof(VisitInfo))]
        public Guid VisitId { get; set; }
        public string SigntureNotes { get; set; }
        public string SigntureImagePath { get; set; }
        public virtual VisitInfo VisitInfo { get; set; }

        public virtual ICollection<VisitSigntureDataImage> VisitSigntureDataImages { get; set; }


    }
    [Table(nameof(VisitSigntureDataImage) + "s", Schema = AppConstants.Areas.Visit)]

    public class VisitSigntureDataImage : BaseEntity
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey(nameof(VisitSignture))]
        public Guid VisitSigntureId { get; set; }

        public virtual VisitSignture VisitSignture { get; set; }

    }


    [Table(nameof(VisitNote) + "s", Schema = AppConstants.Areas.Visit)]

    public class VisitNote : BaseEntity
    {
        [ForeignKey(nameof(VisitInfo))]
        public Guid VisitId { get; set; }
        public string Notes { get; set; }
        public virtual VisitInfo VisitInfo { get; set; }

        public virtual ICollection<VisitNoteImage> VisitNoteImages { get; set; }
    }
 
    [Table(nameof(VisitNoteImage) + "s", Schema = AppConstants.Areas.Visit)]
    public class VisitNoteImage : BaseEntity
    {
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey(nameof(VisitNote))]
        public Guid VisitNoteId { get; set; }

        public virtual VisitNote VisitNote { get; set; }
    }



        [Table(nameof(VisitApproval) + "s", Schema = AppConstants.Areas.Visit)]

    public class VisitApproval : BaseEntity
    {
        [ForeignKey(nameof(VisitInfo))]
        public Guid VisitInfoId { get; set; }

        public string RejectedComment { get; set; }

        public ApprovalCasesEnum ApprovalCaseNo { get; set; }
        public virtual VisitInfo VisitInfo { get; set; }

    }
}
