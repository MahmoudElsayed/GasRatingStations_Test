using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.Common.General;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
    public class VisitApprovalBll
    {
        #region Fields
        private readonly string _spGetVisitApproval = "Visit.spGetVisitApproval";
        private readonly IRepository<VisitApproval> _repoVisitApproval;
        private readonly IRepository<AnswerCategory> _repoAnswerCategory;
        private readonly QuestionsBll _questionsBll;

        private readonly VisitBll _visitBll;
        private readonly ManualDistributionBll _manualDistributionBll;
        private readonly IMapper _mapper;

        public VisitApprovalBll(IRepository<VisitApproval> repoVisitApproval, IRepository<AnswerCategory> repoAnswerCategory,
            QuestionsBll questionsBll, VisitBll visitBll, ManualDistributionBll manualDistributionBll, IMapper mapper)
        {
            _repoVisitApproval = repoVisitApproval;
            _repoAnswerCategory = repoAnswerCategory;
            _questionsBll = questionsBll;
            _visitBll = visitBll;
            _manualDistributionBll = manualDistributionBll;
        }

        #endregion

        #region Helpers
        public ResultViewModel ChangeCase(Guid visitNoId, ApprovalCasesEnum caseNo, string rejectedComment)
        {
            ResultViewModel result = new ResultViewModel();
            result.Status = false;
            result.Message = AppConstants.Messages.SavedFailed;

            var tbl = _repoVisitApproval.GetAllAsNoTracking().Include(p=>p.VisitInfo).Where(p => p.VisitInfoId == visitNoId).FirstOrDefault();
            if (tbl == null)

            {
                tbl = new VisitApproval();
                tbl.ID = Guid.NewGuid();
                tbl.VisitInfoId = visitNoId;
                tbl.AddedBy = _repoVisitApproval.UserId;
                tbl.ModifiedBy = _repoVisitApproval.UserId;
                tbl.ModifiedDate = AppDateTime.Now;
                tbl.ApprovalCaseNo = caseNo;

                tbl.CreatedDate = AppDateTime.Now;
                if (caseNo == ApprovalCasesEnum.Rejected)
                {
                    tbl.RejectedComment = rejectedComment;




                }
                _repoVisitApproval.InsertWithoutSaveChange(tbl);


            }
            else
            {
                tbl.ModifiedBy = _repoVisitApproval.UserId;
                tbl.ModifiedDate = AppDateTime.Now;
                tbl.ApprovalCaseNo = caseNo;
                if (caseNo == ApprovalCasesEnum.Rejected)
                {
                    tbl.RejectedComment = rejectedComment;
                }
                _repoVisitApproval.UpdateWithoutSaveChange(tbl);
            }
            if (_repoVisitApproval.SaveChange())
            {
                if (tbl.ApprovalCaseNo == ApprovalCasesEnum.Rejected)
                {

                  var  VisitInfo = _visitBll.GetAll().Where(p => p.ID == visitNoId).FirstOrDefault();
                    _repoVisitApproval.ExecuteStoredProcedure<bool>($"update [Setting].[ManualDistribution] set IsVisited=1,IsClosed=1 where StationId='{VisitInfo.StationId}'",null, CommandType.Text);

                    _repoVisitApproval.ExecuteStoredProcedure<bool>($"update [Visit].[VisitInfo] set IsClosed=1 where StationId='{VisitInfo.StationId}'", null, CommandType.Text);

                    //اعادة توزيع مع انشاء زيارة

                    if (_manualDistributionBll.Save(new List<ManualDistributionDTO>() { new ManualDistributionDTO() {

                        UserId=VisitInfo.UserId, StationId=VisitInfo.StationId,IsVisited=true, FromVisitApproval=true,IsClosed=false
                    } },true).Status)
                            {
                                _visitBll.Create(new CreateVisitDTO() { StationId = VisitInfo.StationId, UserId = VisitInfo.UserId, FromVisitApproval=true });

                            }


                        

                    
                }
                _repoVisitApproval.Detached(tbl);
                result.Status = true;
                result.Message = AppConstants.Messages.SavedSuccess;

            }
            return result;

        }
        #endregion
        #region LoadData
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoVisitApproval.ExecuteStoredProcedure<VisitApprovalDataTableDTO>
                (_spGetVisitApproval, mdl?.ToSqlParameter(_repoVisitApproval.UserId), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        public IEnumerable<VisitQuestionsDTO> GetQuestions(Guid visitNoId)
        {





            var visit = _visitBll.GetAll()
                .Include(p => p.VisitAnswers).ThenInclude(p => p.VisitAnswerOptions).ThenInclude(p => p.Answer).ThenInclude(p => p.AnswerCategory)
                .Include(p => p.VisitAnswers).ThenInclude(p => p.VisitAnswerOptions).ThenInclude(p => p.Answer).ThenInclude(p => p.Question)
                .Where(p => p.ID == visitNoId).FirstOrDefault();
            if (visit == null)
            {
                return null;
            }
            var questions = visit.VisitAnswers;
            var VisitQuestions = questions.SelectMany(a => a.VisitAnswerOptions).OrderBy(p => p.Answer.Question.OrderNo)
            .Select(o => new GetVisitQuestionsDTO
            {


                QuestionOrderNo = o.Answer.Question.OrderNo ?? 0,
                AnswerOrderNo = o.Answer.OrderNo ?? 0,
                AnswerCategotyOrderNo = o.Answer.AnswerCategoryOrderNo ?? 0,
                QuestionTextId = o.Answer.Question.ID,
                QuestionText = o.Answer.Question.Text,
                CategoryText = o.Answer.AnswerCategory.Text,
                AnswerText = o.Answer.Text

            })?.ToList().OrderBy(p => p.QuestionOrderNo);
            var VisitQuestionsResult = VisitQuestions.GroupBy(p => p.QuestionTextId).Select(p => new VisitQuestionsDTO
            {
                OrderNo = _questionsBll.GetById(p.Key).OrderNo ?? 0,

                QuestionText = VisitQuestions.Where(q => q.QuestionTextId == p.Key).FirstOrDefault().QuestionText,
                lstAnswers = VisitQuestions.Where(v => v.QuestionTextId == p.Key).OrderBy(p => p.AnswerCategotyOrderNo).OrderBy(p => p.AnswerOrderNo).Select(a => new GetVisitQuestionsDTO
                {

                    AnswerText = a.AnswerText,
                    CategoryText = a.CategoryText

                }).ToList()
            });

            return VisitQuestionsResult;
        }
        #endregion
       
        public string GetStationRejectedComment(Guid stationId)
        {
            if (_visitBll.IsStationRejected(stationId))
            {
                var data = _repoVisitApproval.GetAllAsNoTracking().Include(p => p.VisitInfo).Where(p => p.VisitInfo.StationId == stationId).OrderBy(p => p.CreatedDate).LastOrDefault();
                if (data != null)
                {

                    if (data.ApprovalCaseNo == ApprovalCasesEnum.Rejected)
                    {
                        return data.RejectedComment;
                    }
                }
            }
           
            return string.Empty;
        }
    }
}
