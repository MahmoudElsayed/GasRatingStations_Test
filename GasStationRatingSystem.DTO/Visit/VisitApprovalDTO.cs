using GasStationRatingSystem.Common.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class VisitApprovalDataTableDTO
    {
        public int TotalCount { get; set; }
        public Guid VisitInfoId { get; set; }
        public string StationName { get; set; }
        public string InspectorName { get; set; }
        public string VisitTime { get; set; }
        public string RejectedComment { get; set; }
        public int ApprovalCaseNo { get; set; }
        public string ApprovalCaseText => ApprovalCaseNo != -1 ? ((ApprovalCasesEnum)ApprovalCaseNo).GetEnumDescription() : "لم يتخذ عليها اي اجراء";
    }
    public class GetVisitQuestionsDTO
    {
        public int QuestionOrderNo { get; set; } = default;
        public int AnswerCategotyOrderNo { get; set; } = default;

        public int AnswerOrderNo { get; set; } = default;
        public Guid QuestionTextId { get; set; }
        public string QuestionText { get; set; }
        public string CategoryText { get; set; }
        public string AnswerText { get; set; }

    }
    public class VisitQuestionsDTO
    {
        public Guid Id { get; set; }
        public int OrderNo { get; set; }
        public string QuestionText { get; set; }
        public List<GetVisitQuestionsDTO> lstAnswers { get; set; }
    }
}
