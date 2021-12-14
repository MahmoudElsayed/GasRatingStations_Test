using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
    public class VisitBll
    {

        #region Fields

        private readonly IRepository<VisitInfo> _repoVisitInfo;
        private readonly IRepository<VisitAnswer> _repoVisitAnswer;

        private readonly UserBll _userBll;
        private readonly GasStationBll _gasStationBll;
        private readonly UploadBll _uploadBll;

        public VisitBll(IRepository<VisitInfo> repoVisitInfo, IRepository<VisitAnswer> repoVisitAnswer, UserBll userBll, GasStationBll gasStationBll, UploadBll uploadBll)
        {
            _repoVisitInfo = repoVisitInfo;
            _repoVisitAnswer = repoVisitAnswer;
            _userBll = userBll;
            _gasStationBll = gasStationBll;
            _uploadBll = uploadBll;
        }

        #endregion
        public IQueryable<VisitInfo> GetAll() => _repoVisitInfo.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive);
        #region Create And Update Visit
        public ResultDTO Create(CreateVisitDTO para)
        {
            ResultDTO result = new ResultDTO();
            var user = _userBll.GetById(_repoVisitAnswer.UserId);
            var station = _gasStationBll.GetById(para.StationId.Value);

            if (user == null)
            {
                result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.UserNotExists));

            }
            else if (station == null)
            {
                result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.StationNotExists));

            }
            else
            {
                var UserId = _repoVisitInfo.UserId;
                if (para.UserId.HasValue)
                {
                    UserId = para.UserId.Value;
                }
                //var tblVisit = _repoVisitInfo.GetAllAsNoTracking().Where(p => p.StationId == para.StationId.Value && p.UserId == para.UserId && p.CreatedDate.Date == AppDateTime.Now.Date).FirstOrDefault();
                //if (tblVisit != null)
                //{
                //    result.data = new { Visit = new { Id = tblVisit.ID, VisitNo = tblVisit.VisitNo } };

                //    result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
                //    return result;
                //}
                VisitInfo data = new VisitInfo() { UserId = UserId, AddedBy = UserId, StationId = para.StationId.Value, FromVisitApproval = para.FromVisitApproval };
                if (_repoVisitInfo.Insert(data))
                {
                    result.data = new { Visit = new { Id = data.ID, VisitNo = data.VisitNo } };
                    _repoVisitInfo.ExecuteStoredProcedure<bool>($"update [Setting].[ManualDistribution] set IsVisited=1,IsClosed=1 where StationId='{data.StationId}'", null, CommandType.Text);

                    _repoVisitInfo.ExecuteStoredProcedure<bool>($"update [Visit].[VisitInfo] set IsClosed=1 where StationId='{data.StationId}' and ID !='{data.ID}'", null, CommandType.Text);

                    _repoVisitAnswer.ExecuteStoredProcedure<bool>("Guide.UpdateVisitStationStatus", new SqlParameter[] {
                    new SqlParameter("@stationId", station.ID) });
                    result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
             
                
                }
                else
                {
                    result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                }
            }
            return result;
        }
        public bool Update(VisitInfo visit)
        {
            return _repoVisitInfo.Update(visit);
        }

        #endregion
        public bool IsStationRejected(Guid stationId)
        {
            var data = _repoVisitInfo.GetAllAsNoTracking().Where(p => p.StationId == stationId && p.IsClosed == false && p.FromVisitApproval).FirstOrDefault();
            if (data != null)
            {

                return true;
            }
            return false;
        }
        #region Save Visit Answers
        public ResultDTO SaveVisitAnswer(SaveVisitAnswersDTO para)
        {
            ResultDTO result = new ResultDTO();

            List<VisitAnswerOption> visitAnswerOptions = new List<VisitAnswerOption>();
            VisitAnswer data = new VisitAnswer() { VisitId = para.VisitId.Value, AddedBy = _repoVisitAnswer.UserId };
            var visit = _repoVisitInfo.GetAllAsNoTracking().Where(p => p.ID == para.VisitId).FirstOrDefault();
            if (visit == null)
            {
                result.Message = AppConstants.Messages.StationIsNotExists;
                return result;
            }
            //if (_repoVisitAnswer.GetAllAsNoTracking().Include(p=>p.VisitInfo)
            //    .Any(p=>p.VisitId==para.VisitId&&p.VisitInfo.IsClosed))
            //{
            //    visit.PageIndex = 15;
            //    _repoVisitInfo.Update(visit);
            //    result.data = new { };
            //    _repoVisitInfo.ExecuteStoredProcedure<bool>($"update [Visit].[VisitInfo] set IsClosed=1 where  ID ='{visit.ID}'", null, CommandType.Text);

            //    result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
            //    return result;
            //}


            para.Options.ForEach(p => { visitAnswerOptions.Add(new VisitAnswerOption() { AnswerId = p.OptionId, RefId = p.RefId, VisitAnswerId = data.ID }); });
            data.VisitAnswerOptions = visitAnswerOptions;
            _repoVisitAnswer.InsertWithoutSaveChange(data);


            #region حفظ صور الاسئلة
            if (para.QuestionsImages != null)
            {


                VisitQuestionImage visitQuestionImage = new VisitQuestionImage();
                visitQuestionImage.ID = Guid.NewGuid();
                visitQuestionImage.VisitId = visit.ID;
                visitQuestionImage.AddedBy = _repoVisitAnswer.UserId;

                var lstQuestionImages = new List<VisitQuestionImageDetail>();

                foreach (var item in para.QuestionsImages)
                {
                    if (item==null)
                    {
                        continue;
                    }
                    foreach (var image in item.QuestionBase64Images)
                    {

                        if (string.IsNullOrWhiteSpace(image))
                        {
                            continue;
                        }

                        VisitQuestionImageDetail visitQuestionImageDetail = new VisitQuestionImageDetail();

                        visitQuestionImageDetail.ID = Guid.NewGuid();

                        var fileName = visit.ID + "_" + visitQuestionImageDetail.ID + ".png";

                        visitQuestionImageDetail.VisitQuestionImageId = visitQuestionImage.ID;
                        visitQuestionImageDetail.ImageName = fileName;
                        visitQuestionImageDetail.ImagePath = AppConstants.VisitQuestionsPath + fileName;
                       
                        visitQuestionImageDetail.QuestionId = item.QuestionId.Value;
                        visitQuestionImageDetail.AddedBy = _repoVisitAnswer.UserId;

                        if (_uploadBll.Save(visitQuestionImageDetail.ImagePath, image))
                        {
                            lstQuestionImages.Add(visitQuestionImageDetail);
                        }
                    }


                }
                visitQuestionImage.VisitQuestionImageDetails = lstQuestionImages;
                visit.VisitQuestionImage = visitQuestionImage;
            }
            #endregion
            #region حفظ توقيع الزيارة
            if (para.SigntureData != null)
            {


                VisitSignture visitSignture = new VisitSignture();
                visitSignture.ID = Guid.NewGuid();
                visitSignture.VisitId = visit.ID;
                visitSignture.AddedBy = _repoVisitAnswer.UserId;


                visitSignture.SigntureNotes = para.SigntureData.SigntureNotes;
                string SignturePath = AppConstants.VisitSignturesPath + visitSignture.ID + ".png";
                if (_uploadBll.Save(SignturePath, para.SigntureData.SigntureBase64Image))
                {
                    visitSignture.SigntureImagePath = SignturePath;

                }



                var lstVisitSigntureImages = new List<VisitSigntureDataImage>();

                foreach (var image in para.SigntureData.SigntureNotesBase64Images)
                {

                    if (string.IsNullOrWhiteSpace(image))
                    {
                        continue;
                    }


                    VisitSigntureDataImage visitSigntureDataImage = new VisitSigntureDataImage();

                    visitSigntureDataImage.ID = Guid.NewGuid();

                    var fileName = visit.ID + "_" + visitSigntureDataImage.ID + ".png";

                    visitSigntureDataImage.VisitSigntureId = visitSignture.ID;
                    visitSigntureDataImage.ImageName = fileName;
                    visitSigntureDataImage.ImagePath = AppConstants.VisitSignturesPath + fileName;
                    visitSigntureDataImage.AddedBy = _repoVisitAnswer.UserId;

                    if (_uploadBll.Save(visitSigntureDataImage.ImagePath, image))
                    {
                        lstVisitSigntureImages.Add(visitSigntureDataImage);
                    }



                }
                visitSignture.VisitSigntureDataImages = lstVisitSigntureImages;
                visit.VisitSignture = visitSignture;
            }
            #endregion
            #region حفظ ملاحظات الزيارة
            if (para.VisitNotesData != null)
            {


                VisitNote visitNote = new VisitNote();
                visitNote.ID = Guid.NewGuid();
                visitNote.VisitId = visit.ID;
                visitNote.AddedBy = _repoVisitAnswer.UserId;


                visitNote.Notes = para.VisitNotesData.VisitNotes;
                



                var lstVisitNoteImages = new List<VisitNoteImage>();

                foreach (var image in para.VisitNotesData.VisitBase64Images)
                {

                    if (string.IsNullOrWhiteSpace(image))
                    {
                        continue;
                    }


                    VisitNoteImage visitNoteImage = new VisitNoteImage();

                    visitNoteImage.ID = Guid.NewGuid();

                    var fileName = visit.ID + "_" + visitNoteImage.ID + ".png";

                    visitNoteImage.VisitNoteId = visitNote.ID;
                    visitNoteImage.ImageName = fileName;
                    visitNoteImage.ImagePath = AppConstants.VisitNotesImagesPath + fileName;
                    visitNoteImage.AddedBy = _repoVisitAnswer.UserId;

                    if (_uploadBll.Save(visitNoteImage.ImagePath, image))
                    {
                        lstVisitNoteImages.Add(visitNoteImage);
                    }



                }
                visitNote.VisitNoteImages = lstVisitNoteImages;
                visit.VisitNote = visitNote;
            }
            #endregion

            if (_repoVisitAnswer.SaveChange())
            {
                if (visit != null)
                {
                    visit.PageIndex = 15;
                    visit.UserId = _repoVisitAnswer.UserId;
                    visit.AddedBy = visit.UserId;
                    _repoVisitInfo.Update(visit);
                    _repoVisitInfo.Detached(visit);
                }
                _repoVisitInfo.ExecuteStoredProcedure<bool>($"update [Visit].[VisitInfo] set IsClosed=1 where  ID ='{visit.ID}'", null, CommandType.Text);

                _repoVisitAnswer.ExecuteStoredProcedure<bool>("Guide.UpdateVisitStationStatus", new SqlParameter[] { 
                    new SqlParameter("@stationId", visit.StationId) });


                result.data = new { };
                result.Message = AppConstants.Messages.SavedSuccess;
                    //_repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
            }
            else
            {
                result.Message = AppConstants.Messages.SavedFailed;

                // result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
            }

            return result;
        }
        #endregion
        /// <summary>
        /// حذف اسئلة التقييم الخاصة بزيارة معينة
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        public ResultDTO DeleteVisitAnswers(Guid visitId)
        {
            ResultDTO result = new ResultDTO();

            var visit = _repoVisitInfo.GetAllAsNoTracking().Where(p => p.ID == visitId).FirstOrDefault();
            if (visit == null)
            {
                result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
                return result;
            }

            var rows = _repoVisitInfo.ExecuteStoredProcedure<DeleteVisitAnswersDTO>("Visit.DeleteVisitAnswers", new SqlParameter[] { new SqlParameter("@visitId", visitId) }).FirstOrDefault();
            if (rows.RowsAffected == 1)
            {

                result.data = new { };
                result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.SuccessfullyDone));
            }
            else
            {
                result.Message = _repoVisitInfo.HttpContextAccessor.HttpContext.GetLocalizedString(nameof(Resources.GasStationRatingSystemResources.ErrorExists));
            }

            return result;
        }


        #region GetVisits
        public ResultDTO GetVisits()
        {
            ResultDTO result = new ResultDTO();
            result.data = _repoVisitInfo.GetAllAsNoTracking().Include(p => p.GasStation).ThenInclude(p => p.District).Include(p => p.GasStation).ThenInclude(p => p.ManualDistributions)
                .Where(p => p.IsActive && !p.IsDeleted && p.IsClosed == false && !string.IsNullOrEmpty(p.GasStation.Name) && p.GasStation.ManualDistributions.Any(m => m.UserId == _repoVisitInfo.UserId) && p.PageIndex < 15).Select(p => new
                {
                    VisitId = p.ID,
                    PageIndex = p.PageIndex,
                    VisitNo = p.VisitNo,
                    VisitTime = p.VisitTime.ToString("dd/MM/yyyy hh:mm:ss tt"),
                    StationName = $"{p.GasStation.Name}-{p.GasStation.District.Name}",
                    Latitude = p.GasStation.Lat,
                    Longitude = p.GasStation.Lng
                });
            return result;
        }

        #endregion
        #region Helpers
        public bool OptionIsSelected(Guid stationId, Guid answerId)
        {

            var visitInfo = _repoVisitInfo.GetAll().Where(p => p.StationId == stationId && p.IsClosed).OrderBy(p => p.CreatedDate).LastOrDefault();
            if (visitInfo != null)
            {

                return _repoVisitInfo.ExecuteStoredProcedure<CountQueryDTO>($"  select COUNT(*) CountData  from Visit.VisitAnswerOptions vso where  vso.AnswerId='{answerId}' and vso.VisitAnswerId in(select va.ID from Visit.VisitAnswers va where va.VisitId='{visitInfo.ID}')", null, CommandType.Text).FirstOrDefault()?.CountData > 0;
                //             return _repoVisitAnswer.GetAllAsNoTracking()
                //.Include(p => p.VisitInfo).Include(p => p.VisitAnswerOptions)
                //.Where(p => !p.IsDeleted && p.IsActive)
                //.Any(p => p.VisitInfo.StationId == stationId && p.VisitAnswerOptions.Any(ans => ans.AnswerId == answerId));

            }
            return false;
        }
        public class CountQueryDTO
        {
            public int CountData { get; set; }
        }
        #endregion

    }
}
