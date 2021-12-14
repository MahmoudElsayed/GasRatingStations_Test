using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.Tables;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
    public class QuestionsBll
    {
        #region Fields
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<Question> _repoQuestion;
        private readonly IRepository<Answer> _repoAnswer;
        private readonly IRepository<AnswerCategory> _repoAnswerCategory;
        private readonly IRepository<ManualDistribution> _repoManualDistribution;
        private readonly VisitBll _visitBll;
        private readonly IMapper _mapper;
        private readonly AppFormBll  _appFormBll;
        private readonly GasStationBll _gasStationBll;
        private CurrentFormDTO  currentFormDTO;

        public QuestionsBll(IRepository<Question> repoQuestion, IRepository<Answer> repoAnswer, IRepository<AnswerCategory> repoAnswerCategory, IRepository<ManualDistribution> repoManualDistribution, VisitBll visitBll, GasStationBll gasStationBll, IWebHostEnvironment webHostEnvironment, IMapper mapper, AppFormBll appFormBll)
        {
            _repoQuestion = repoQuestion;
            _repoAnswer = repoAnswer;
            _repoAnswerCategory = repoAnswerCategory;
            _repoManualDistribution = repoManualDistribution;
            _visitBll = visitBll;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _appFormBll = appFormBll;
            _gasStationBll = gasStationBll;
        }

        

        #endregion
        #region Get


        public IQueryable<QuestionsOrderDTO> GetQuestionsByParentId(Guid? id)
            => _repoQuestion.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive && p.ParentId == id).OrderBy(p => p.OrderNo).Select(p=>new QuestionsOrderDTO
            {
                 Id=p.ID,
                 Text=p.Text,
                 OrderNo=p.OrderNo??0
            });
        public string GetTextOneOfQuestion(Guid questionId, int questionParentNo)
        {
            var QestionText = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == questionId && a.IsLast == false && a.QuestionParentNo == questionParentNo).FirstOrDefault()?.Text ?? "";
            return QestionText;
        }
        public IQueryable<SelectListDTO> GetSelect()
        {
            return _repoQuestion.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive).Select(p => new SelectListDTO()
            {
                Value = p.ID,
                Text = p.Text
            });
        }
        public IQueryable<SelectListDTO> GetDashboardSettingSelect()
        {
            //حالة المعيار فقط
            return _repoQuestion.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive&&p.Answers.Any(a=>a.AnswerCategoryId==Guid.Parse("6F3FDFC4-3F14-4B9B-9FB1-F9785C424C56"))).Include(p=>p.Answers).Select(p => new SelectListDTO()
            {
                Value = p.ID,
                Text = p.Text
            });
        }
        public IEnumerable<Question> GetAll() => _repoQuestion.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive && (p.IsLast ?? false)).OrderBy(p=>p.OrderNo);

        public IEnumerable<SelectListDTO> GetAnswersCategories()
        {
            return _repoAnswerCategory.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive&&!string.IsNullOrEmpty(p.Text)).Select(p => new SelectListDTO
            {
                 Text=p.Text,
                 Value=p.ID

            });
        }
        public ResultDTO GetQuestions()
        {

            ResultDTO result = new ResultDTO();
         

            var lst = new List<ParentQuestionDTO>();
            int PageIndex = 1;
            _repoQuestion.GetAllAsNoTracking().Include(p => p.tblQuestion).Where(p => p.IsActive && !p.IsDeleted && !p.ParentId.HasValue).OrderBy(p => p.OrderNo).ToList().ForEach(
               p =>
               {

                   var Items = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && !p.IsDeleted && a.IsLast == false && a.QuestionParentNo.Value == 1).OrderBy(p => p.OrderNo);
                   if (Items.Count() != 0)
                   {
                       foreach (var Item in Items)
                       {



                           var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == Item.ID && a.IsLast == false && a.QuestionParentNo.Value == 2 && !p.IsDeleted).OrderBy(p => p.OrderNo)?.ToList();
                           if (Item2.Count() > 0)
                           {


                               foreach (var item2 in Item2)
                               {


                                   var i2 = item2;

                                   if (item2 == null)
                                   {
                                       i2 = Item;

                                   }
                                   string Item2Text = i2?.Text ?? "";




                                   lst.Add(
                           new ParentQuestionDTO
                           {
                               PageIndex = PageIndex++,
                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                               // ItemOneId = Item?.ID??null,
                               // ItemTwoId = Item2?.ID ?? null,
                               ItemOneText = Item.Text,
                               Items = new ItemsDTO
                               {

                                   ItemTwoText = Item2Text,
                                   Questions =
                               _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (i2 != null ? i2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList().Select(
                                   q => new QuestionsDTO
                                   {
                                       Id = q.ID,
                                       QuestionText = q.Text,
                                       QuestionTypeNo = (int)q.QuestionType,
                                       MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                                       ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                                       Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID&&!an.IsDeleted).OrderBy(p => p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                       new AnswerDTO
                                       {
                                           AnswerCategoryId = Guid.NewGuid(),
                                           Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                            ,
                                                                                        Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                           op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = false }
                                           ).ToList()
                                       }).ToList()
                                   }
                                   ).ToList()
                               }



                           });



                               }
                           }
                           else
                           {
                               lst.Add(
               new ParentQuestionDTO
               {
                   PageIndex = PageIndex++,
                   // ParentItemId = p.ID,
                   ParentItemText = p.Text,
                   // ItemOneId = Item?.ID??null,
                   // ItemTwoId = Item2?.ID ?? null,
                   ItemOneText = Item.Text,
                   Items = new ItemsDTO
                   {

                       ItemTwoText = Item.Text,
                       Questions =
                   _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (Item != null ? Item.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList().Select(
                       q => new QuestionsDTO
                       {
                           Id = q.ID,
                           QuestionText = q.Text,
                           QuestionTypeNo = (int)q.QuestionType,
                           MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                           ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                           Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).OrderBy(p => p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                           new AnswerDTO
                           {
                               AnswerCategoryId = Guid.NewGuid(),
                               Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                ,
                                                                            Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                           op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = false }
                               ).ToList()
                           }).ToList()
                       }
                       ).ToList()
                   }



               });
                           }



                       }
                   }
                   else
                   {
                       var Item = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 1).OrderBy(p => p.OrderNo).FirstOrDefault();
                       var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 2).OrderBy(p => p.OrderNo).FirstOrDefault();


                       if (Item2 == null)
                       {
                           Item2 = Item;
                       }
                       lst.Add(
                           new ParentQuestionDTO
                           {
                               PageIndex = PageIndex++,

                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                               // ItemOneId = Item?.ID??null,
                               // ItemTwoId = Item2?.ID ?? null,
                               ItemOneText = GetTextOneOfQuestion(p.ID, 1),
                               Items = new ItemsDTO
                               {
                                   ItemTwoText = GetTextOneOfQuestion(p.ID, 2),
                                   Questions =
                               _repoQuestion.GetAllAsNoTracking().Include(p => p.Answers).Where(qu => qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted
                               ).OrderBy(p => p.OrderNo).ToList().Select(
                                   q => new QuestionsDTO
                                   {
                                       Id = q.ID,
                                       QuestionText = q.Text,
                                       QuestionTypeNo = (int)q.QuestionType,

                                       MultipleAnswerCategory = q.HasMultiCategoryAnswer,

                                       Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                         new AnswerDTO
                                         {
                                             AnswerCategoryId = Guid.NewGuid(),
                                             Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                               ,
                                                                                          Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                           op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = false }
                                             ).ToList()
                                         }).ToList()
                                   }
                                   ).ToList()
                               }



                           });
                   }
               }
               );
            result.data = new {  Items = lst };


            return result;
        }

        public ResultDTO GetQuestionsByVisit(Guid visitId)
        {
            
            ResultDTO result = new ResultDTO();
            var visit=_visitBll.GetAll().Where(p=>p.ID==visitId).Include(p=>p.User).FirstOrDefault();
            Guid stationId = Guid.Empty;
            if (visit == null)
            {
               // result.Message = "معرف الزيارة غير موجود";
               // return result;
            }
            else
            {
                 stationId = visit.StationId;

            }

            var lst = new List<ParentQuestionDTO>();
            int PageIndex = 1;
            _repoQuestion.GetAllAsNoTracking().Include(p => p.tblQuestion).Where(p => p.IsActive && !p.IsDeleted && !p.ParentId.HasValue).OrderBy(p => p.OrderNo).ToList().ForEach(
               p =>
               {

                   var Items = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && !p.IsDeleted && a.IsLast == false && a.QuestionParentNo.Value == 1).OrderBy(p => p.OrderNo);
                   if (Items.Count() != 0)
                   {
                       foreach (var Item in Items)
                       {



                           var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == Item.ID && a.IsLast == false && a.QuestionParentNo.Value == 2 && !p.IsDeleted).OrderBy(p => p.OrderNo)?.ToList();
                           if (Item2.Count() > 0)
                           {


                               foreach (var item2 in Item2)
                               {


                                   var i2 = item2;

                                   if (item2 == null)
                                   {
                                       i2 = Item;

                                   }
                                   string Item2Text = i2?.Text ?? "";




                                   lst.Add(
                           new ParentQuestionDTO
                           {
                               PageIndex = PageIndex++,
                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                               // ItemOneId = Item?.ID??null,
                               // ItemTwoId = Item2?.ID ?? null,
                               ItemOneText = Item.Text,
                               Items = new ItemsDTO
                               {

                                   ItemTwoText = Item2Text,
                                   Questions =
                               _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (i2 != null ? i2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList().Select(
                                   q => new QuestionsDTO
                                   {
                                       Id = q.ID,
                                       QuestionText = q.Text,
                                       QuestionTypeNo = (int)q.QuestionType,
                                       MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                                       ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                                       Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).OrderBy(p => p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                       new AnswerDTO
                                       {
                                           AnswerCategoryId = Guid.NewGuid(),
                                           Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                            ,
                                                                                        Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                           op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = _visitBll.OptionIsSelected(stationId, op.ID) }
                                           ).ToList()
                                       }).ToList()
                                   }
                                   ).ToList()
                               }



                           });



                               }
                           }
                           else
                           {
                               lst.Add(
               new ParentQuestionDTO
               {
                   PageIndex = PageIndex++,
                   // ParentItemId = p.ID,
                   ParentItemText = p.Text,
                   // ItemOneId = Item?.ID??null,
                   // ItemTwoId = Item2?.ID ?? null,
                   ItemOneText = Item.Text,
                   Items = new ItemsDTO
                   {

                       ItemTwoText = Item.Text,
                       Questions =
                   _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (Item != null ? Item.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList().Select(
                       q => new QuestionsDTO
                       {
                           Id = q.ID,
                           QuestionText = q.Text,
                           QuestionTypeNo = (int)q.QuestionType,
                           MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                           ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                           Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).OrderBy(p => p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                           new AnswerDTO
                           {
                               AnswerCategoryId = Guid.NewGuid(),
                               Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                ,
                                                                            Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                           op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = _visitBll.OptionIsSelected(stationId, op.ID) }
                               ).ToList()
                           }).ToList()
                       }
                       ).ToList()
                   }



               });
                           }



                       }
                   }
                   else
                   {
                       var Item = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 1).FirstOrDefault();
                       var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 2).FirstOrDefault();


                       if (Item2 == null)
                       {
                           Item2 = Item;
                       }
                       lst.Add(
                           new ParentQuestionDTO
                           {
                               PageIndex = PageIndex++,

                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                               // ItemOneId = Item?.ID??null,
                               // ItemTwoId = Item2?.ID ?? null,
                               ItemOneText = GetTextOneOfQuestion(p.ID, 1),
                               Items = new ItemsDTO
                               {
                                   ItemTwoText = GetTextOneOfQuestion(p.ID, 2),
                                   Questions =
                               _repoQuestion.GetAllAsNoTracking().Include(p => p.Answers).Where(qu => qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted
                               ).OrderBy(p => p.OrderNo).ToList().Select(
                                   q => new QuestionsDTO
                                   {
                                       Id = q.ID,
                                       QuestionText = q.Text,
                                       QuestionTypeNo = (int)q.QuestionType,

                                       MultipleAnswerCategory = q.HasMultiCategoryAnswer,

                                       Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                         new AnswerDTO
                                         {
                                             AnswerCategoryId = Guid.NewGuid(),
                                             Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                               ,
                                                                                          Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                           op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = _visitBll.OptionIsSelected(stationId, op.ID) }
                                             ).ToList()
                                         }).ToList()
                                   }
                                   ).ToList()
                               }



                           });
                   }
               }
               );
            var stationData = _gasStationBll.GetAll().Where(s=>s.ID==stationId).Include(p=>p.District).FirstOrDefault();
            result.data = new { Station = stationData != null?  new{Id=stationData.ID,Name=stationData.Name.TrimEnd()
              ,StationCode=stationData.Code,  DistrictName=stationData.District.Name,InspectorName=string.IsNullOrWhiteSpace( visit.User.InspectorName)?visit.User.UserName:visit.User.InspectorName
             , Latitude=stationData.Lat,Longitude=stationData.Lng,VisitTime=visit.VisitTime.ToString("dd/MM/yyyy hh:mm:ss tt") }:null,Items = lst };


            return result;
        }
                public List<QuestionsDTO> GetQuestions2()
                 {

                    var lst = new List<QuestionsDTO>();
        int PageIndex = 1;
        _repoQuestion.GetAllAsNoTracking().Include(p => p.tblQuestion).Where(p => p.IsActive && !p.IsDeleted && !p.ParentId.HasValue).OrderBy(p => p.OrderNo).ToList().ForEach(
           p =>
                       {

            var Items = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && !p.IsDeleted && a.IsLast == false && a.QuestionParentNo.Value == 1).OrderBy(p => p.OrderNo);
            if (Items.Count() != 0)
            {
                foreach (var Item in Items)
                {



                    var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == Item.ID && a.IsLast == false && a.QuestionParentNo.Value == 2 && !p.IsDeleted).OrderBy(p => p.OrderNo)?.ToList();
                    if (Item2.Count() > 0)
                    {


                        foreach (var item2 in Item2)
                        {


                            var i2 = item2;

                            if (item2 == null)
                            {
                                i2 = Item;

                            }
                            string Item2Text = i2?.Text ?? "";

                            lst.AddRange(
                                 _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (i2 != null ? i2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList()
                                 .Select(p => new QuestionsDTO { Id = p.ID, QuestionText = p.Text })
                                );






                        }
                    }
                    else
                    {

                        lst.AddRange(
_repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (Item != null ? Item.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList().Select(p => new QuestionsDTO { Id = p.ID, QuestionText = p.Text })
                            );




                    }



                }
            }
            else
            {
                var Item = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 1).FirstOrDefault();
                var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 2).FirstOrDefault();


                if (Item2 == null)
                {
                    Item2 = Item;
                }

                lst.AddRange(
                        _repoQuestion.GetAllAsNoTracking().Include(p => p.Answers).Where(qu => qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList()
.Select(p => new QuestionsDTO { Id = p.ID, QuestionText = p.Text }));


            }
        }
                       );


                    var ids = lst.Select(p => p.Id.ToString());
        string a = "";
                    foreach (var item in ids)
                    {
                        a += $"'{item}',";
                    }
                    return lst;
                }
public ResultDTO GetAnsweredQuestions(Guid stationId)
        {

            ResultDTO result = new ResultDTO();
            var lst = new List<ParentQuestionDTO>();
            int PageIndex = 1;
            _repoQuestion.GetAllAsNoTracking().Include(p => p.tblQuestion).Where(p => p.IsActive && !p.IsDeleted && !p.ParentId.HasValue).OrderBy(p => p.OrderNo).ToList().ForEach(
               p =>
               {

                   var Items = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && !p.IsDeleted && a.IsLast == false && a.QuestionParentNo.Value == 1).OrderBy(p => p.OrderNo);
                   if (Items.Count() != 0)
                   {
                       foreach (var Item in Items)
                       {



                           var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == Item.ID && a.IsLast == false && a.QuestionParentNo.Value == 2 && !p.IsDeleted).OrderBy(p => p.OrderNo)?.ToList();
                           if (Item2.Count() > 0)
                           {


                               foreach (var item2 in Item2)
                               {


                                   var i2 = item2;

                                   if (item2 == null)
                                   {
                                       i2 = Item;

                                   }
                                   string Item2Text = i2?.Text ?? "";




                                   lst.Add(
                           new ParentQuestionDTO
                           {
                               PageIndex = PageIndex++,
                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                               // ItemOneId = Item?.ID??null,
                               // ItemTwoId = Item2?.ID ?? null,
                               ItemOneText = Item.Text,
                               Items = new ItemsDTO
                               {

                                   ItemTwoText = Item2Text,
                                   Questions =
                               _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (i2 != null ? i2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList().Select(
                                   q => new QuestionsDTO
                                   {
                                       Id = q.ID,
                                       QuestionText = q.Text,
                                       QuestionTypeNo = (int)q.QuestionType,
                                       MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                                       ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                                       Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).OrderBy(p => p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                       new AnswerDTO
                                       {
                                           AnswerCategoryId = Guid.NewGuid(),
                                           Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                            ,
                                           Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                           op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = _visitBll.OptionIsSelected(stationId, op.ID) }
                                           ).ToList()
                                       }).ToList()
                                   }
                                   ).ToList()
                               }



                           });



                               }
                           }
                           else
                           {
                               lst.Add(
               new ParentQuestionDTO
               {
                   PageIndex = PageIndex++,
                   // ParentItemId = p.ID,
                   ParentItemText = p.Text,
                   // ItemOneId = Item?.ID??null,
                   // ItemTwoId = Item2?.ID ?? null,
                   ItemOneText = Item.Text,
                   Items = new ItemsDTO
                   {

                       ItemTwoText = Item.Text,
                       Questions =
                   _repoQuestion.GetAllAsNoTracking().Where(qu => qu.ParentId == (Item != null ? Item.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).OrderBy(p => p.OrderNo).ToList().Select(
                       q => new QuestionsDTO
                       {
                           Id = q.ID,
                           QuestionText = q.Text,
                           QuestionTypeNo = (int)q.QuestionType,
                           MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                           ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                           Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).OrderBy(p => p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                           new AnswerDTO
                           {
                               AnswerCategoryId = Guid.NewGuid(),
                               Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                ,
                                                                            Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                               op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = _visitBll.OptionIsSelected(stationId, op.ID) }
                               ).ToList()
                           }).ToList()
                       }
                       ).ToList()
                   }



               });
                           }



                       }
                   }
                   else
                   {
                       var Item = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 1).FirstOrDefault();
                       var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 2).FirstOrDefault();


                       if (Item2 == null)
                       {
                           Item2 = Item;
                       }
                       lst.Add(
                           new ParentQuestionDTO
                           {
                               PageIndex = PageIndex++,

                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                               // ItemOneId = Item?.ID??null,
                               // ItemTwoId = Item2?.ID ?? null,
                               ItemOneText = GetTextOneOfQuestion(p.ID, 1),
                               Items = new ItemsDTO
                               {
                                   ItemTwoText = GetTextOneOfQuestion(p.ID, 2),
                                   Questions =
                               _repoQuestion.GetAllAsNoTracking().Include(p => p.Answers).Where(qu => qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted
                               ).OrderBy(p => p.OrderNo).ToList().Select(
                                   q => new QuestionsDTO
                                   {
                                       Id = q.ID,
                                       QuestionText = q.Text,
                                       QuestionTypeNo = (int)q.QuestionType,

                                       MultipleAnswerCategory = q.HasMultiCategoryAnswer,

                                       Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                         new AnswerDTO
                                         {
                                             AnswerCategoryId = Guid.NewGuid(),
                                             Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                               ,
                                             Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                             op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text, IsSelected = _visitBll.OptionIsSelected(stationId, op.ID) }
                                             ).ToList()
                                         }).ToList()
                                   }
                                   ).ToList()
                               }



                           });
                   }
               }
               );
            result.data = new { Items = lst };


            return result;
        }



        public ResultDTO GetPendingQuestions(Guid visitId)
        {
            ResultDTO result = new ResultDTO();

            var lst = new List<ParentQuestionDTO>();
            int PageIndex = 1;
            var visit = _visitBll.GetAll().Where(p => p.ID == visitId).Include(p => p.VisitAnswers).ThenInclude(p => p.VisitAnswerOptions).ThenInclude(p => p.Answer).ThenInclude(p => p.Question).FirstOrDefault();
            if (visit != null)
            {


                var VisitQuestionsIds = _repoQuestion.ExecuteStoredProcedure<GetQusetionIdDTO>($"  select distinct (select top 1 qq.OrderNo from Guide.Questions qq where qq.ID=(select  top 1 a.QuestionId from Guide.Answers a where a.Id=vso.AnswerId))OrderNo, (select  top 1 a.QuestionId from Guide.Answers a where a.Id=vso.AnswerId) QuestionId from Visit.VisitAnswerOptions vso where vso.VisitAnswerId in(select vs.ID from Visit.VisitAnswers vs where vs.VisitId='{visit.ID}')", null, System.Data.CommandType.Text).AsEnumerable().OrderBy(p=>p.OrderNo);


                _repoQuestion.GetAllAsNoTracking().Include(p => p.tblQuestion).Where(p => p.IsActive && !p.IsDeleted && !p.ParentId.HasValue).OrderBy(p => p.OrderNo).ToList().ForEach(
                   p =>
                   {

                       var Items = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && !p.IsDeleted && a.IsLast == false && a.QuestionParentNo.Value == 1);
                       if (Items.Count() != 0)
                       {
                           foreach (var Item in Items)
                           {



                               var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == Item.ID && a.IsLast == false && a.QuestionParentNo.Value == 2 && !p.IsDeleted)?.ToList();
                               if (Item2.Count() > 0)
                               {


                                   foreach (var item2 in Item2)
                                   {


                                       var i2 = item2;

                                       if (item2 == null)
                                       {
                                           i2 = Item;

                                       }
                                       string Item2Text = i2?.Text ?? "";




                                       lst.Add(
                               new ParentQuestionDTO
                               {
                                   PageIndex = PageIndex++,
                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                               // ItemOneId = Item?.ID??null,
                               // ItemTwoId = Item2?.ID ?? null,
                               ItemOneText = Item.Text,
                                   Items = new ItemsDTO
                                   {

                                       ItemTwoText = Item2Text,
                                       Questions =
                                   _repoQuestion.GetAllAsNoTracking().Where(qu =>  qu.ParentId == (i2 != null ? i2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).ToList().Where(qu=> !VisitQuestionsIds.Any(v => v.QuestionId == qu.ID)).Select(
                                       q => new QuestionsDTO
                                       {
                                           Id = q.ID,
                                           QuestionText = q.Text,
                                           QuestionTypeNo = (int)q.QuestionType,
                                           MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                                           ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                                           Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).OrderBy(p => p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                           new AnswerDTO
                                           {
                                               AnswerCategoryId = Guid.NewGuid(),
                                               Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                                ,
                                                                                            Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                               op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text }
                                               ).ToList()
                                           }).ToList()
                                       }
                                       ).ToList()
                                   }



                               });



                                   }
                               }
                               else
                               {
                                   lst.Add(
                   new ParentQuestionDTO
                   {
                       PageIndex = PageIndex++,
                   // ParentItemId = p.ID,
                   ParentItemText = p.Text,
                   // ItemOneId = Item?.ID??null,
                   // ItemTwoId = Item2?.ID ?? null,
                   ItemOneText = Item.Text,
                       Items = new ItemsDTO
                       {

                           ItemTwoText = Item.Text,
                           Questions =
                       _repoQuestion.GetAllAsNoTracking().Where(qu =>  qu.ParentId == (Item != null ? Item.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted).ToList().Where(qu => !VisitQuestionsIds.Any(v => v.QuestionId == qu.ID)).Select(
                           q => new QuestionsDTO
                           {
                               Id = q.ID,
                               QuestionText = q.Text,
                               QuestionTypeNo = (int)q.QuestionType,
                               MultipleAnswerCategory = q.HasMultiCategoryAnswer,
                               ListOfReference = q.QuestionType == QuestionType.DropdownLicensedInstitution ? new List<object>() { new { Id = Guid.NewGuid(), Text = "جهة 1" }, new { Id = Guid.NewGuid(), Text = "جهة2" } } : null,
                               Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).OrderBy(p => p.AnswerCategoryOrderNo).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                               new AnswerDTO
                               {
                                   AnswerCategoryId = Guid.NewGuid(),
                                   Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                    ,
                                                                                Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                   op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text }
                                   ).ToList()
                               }).ToList()
                           }
                           ).ToList()
                       }



                   });
                               }



                           }
                       }
                       else
                       {
                           var Item = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 1).FirstOrDefault();
                           var Item2 = _repoQuestion.GetAllAsNoTracking().Where(a => a.ParentId == p.ID && a.IsLast == false && a.QuestionParentNo == 2).FirstOrDefault();


                           if (Item2 == null)
                           {
                               Item2 = Item;
                           }
                           lst.Add(
                               new ParentQuestionDTO
                               {
                                   PageIndex = PageIndex++,

                               // ParentItemId = p.ID,
                               ParentItemText = p.Text,
                               // ItemOneId = Item?.ID??null,
                               // ItemTwoId = Item2?.ID ?? null,
                               ItemOneText = GetTextOneOfQuestion(p.ID, 1),
                                   Items = new ItemsDTO
                                   {
                                       ItemTwoText = GetTextOneOfQuestion(p.ID, 2),
                                       Questions =
                                   _repoQuestion.GetAllAsNoTracking().Include(p => p.Answers).Where(qu =>  qu.ParentId == (Item2 != null ? Item2.ID : p.ID) && qu.IsLast == true && !qu.IsDeleted
                                   ).ToList().Where(qu => !VisitQuestionsIds.Any(v => v.QuestionId == qu.ID)).Select(
                                       q => new QuestionsDTO
                                       {
                                           Id = q.ID,
                                           QuestionText = q.Text,
                                           QuestionTypeNo = (int)q.QuestionType,

                                           MultipleAnswerCategory = q.HasMultiCategoryAnswer,

                                           Answers = _repoAnswer.GetAllAsNoTracking().Where(an => an.QuestionId == q.ID && !an.IsDeleted).ToList().GroupBy(p => p.AnswerCategoryId).Select(AnswerCategory =>
                                             new AnswerDTO
                                             {
                                                 AnswerCategoryId = Guid.NewGuid(),
                                                 Label = _repoAnswerCategory.GetAllAsNoTracking().Where(cat => cat.ID == AnswerCategory.Key).FirstOrDefault().Text
                                                   ,
                                                                                              Options = _repoAnswer.GetAllAsNoTracking().Where(Options => Options.QuestionId == q.ID && !Options.IsDeleted && Options.AnswerCategoryId == AnswerCategory.Key).ToList().OrderBy(p => p.OrderNo).Select(

                                                 op => new OptionsAnswerDTO { OptionId = op.ID, OptionText = op.Text }
                                                 ).ToList()
                                             }).ToList()
                                       }
                                       ).ToList()
                                   }



                               });
                       }
                   }
                   );
            }


            
            result.data = new { Items = lst };
            return result;
        }
        public ResultDTO GetVisits()
        {
            ResultDTO result = new ResultDTO();
            var _repoVisitInfo = _repoQuestion.HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(IRepository<VisitInfo>)) as IRepository<VisitInfo>;
            result.data = _repoVisitInfo.GetAllAsNoTracking().Include(p => p.GasStation).ThenInclude(p => p.District).Where(p => p.IsActive && !p.IsDeleted && _repoManualDistribution.GetAllAsNoTracking().Count(s => s.UserId == _repoAnswer.UserId && s.StationId == p.StationId) == 1 && p.PageIndex < 15).Select(p => new
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

        public Question GetById(Guid id)
        {

            return _repoQuestion.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
        }
        #region GetQuestionsTreeView
        string query = "";
        int index = 1;
       

        public ResultViewModel GetQuestionsTreeView()


        {
            currentFormDTO = _appFormBll.GetCurrentForm();

            ResultViewModel result = new ResultViewModel();

            List<QuestionsTreeViewDTO> lst = new List<QuestionsTreeViewDTO>();


            foreach (var item in _repoQuestion.GetAllAsNoTracking().Where(p => !p.ParentId.HasValue&&!p.IsDeleted).Include(p => p.Answers).ThenInclude(p => p.AnswerCategory).Include(p => p.Questions).OrderBy(p => p.OrderNo))
            {

                lst.Add(new QuestionsTreeViewDTO()
                {
                    Id = item.ID,
                    State = new
                    {
                        Expanded = true,
                        Color = (item.IsLast ?? false) ? "#00ffbf" : "#ffffff"
                    },
                    Text = item.Text + AppendChildOperationBotton(item),
                    Nodes = GetChildrens(item.ID)
                });

            }

            if (!string.IsNullOrEmpty(query))
            {
                _repoQuestion.ExecuteStoredProcedure<int>(query, null, System.Data.CommandType.Text);
                query = string.Empty;
                index = 1;
            }
            result.Data = lst;
            result.Status = true;
            return result;

        }
       
        public List<QuestionsTreeViewDTO> GetChildrens(Guid parentId)
        {
            List<QuestionsTreeViewDTO> lst = new List<QuestionsTreeViewDTO>();

         


            foreach (var item in _repoQuestion.GetAllAsNoTracking().Where(p => p.ParentId==parentId && !p.IsDeleted).Include(p=>p.Answers).ThenInclude(p=>p.AnswerCategory).Include(p=>p.Questions).OrderBy(p => p.OrderNo))
            {

                if (item.IsLast.Value)
                {
                    query += $"update [Guide].[Questions] set OrderNo={index++} where ID='{item.ID}'"+Environment.NewLine;
                }
                lst.Add(new QuestionsTreeViewDTO() { Id = item.ID, State=new { Expanded=true
                },
                    Text = item.Text+ AppendChildOperationBotton(item), Nodes = (item.IsLast??false)?null: GetChildrens(item.ID)});

            }
            return lst;

        }
        private string AppendAddNewParentBotton(Question question)
        {

            string add = currentFormDTO.CurrentFormPermissionActions.AddHasPermission ? $"<button type='button' data-type='parent' data-parentid='{0}'  data-id='{question.ID}'  id='btnAdd_{question.ID}' data-text='{question.Text}' data-operation='add' class='btn btn-primary' onclick='SaveQuestionlModal(this)' >" +
                "<i class='fas fa-plus'></i></button>" : "";

            string edit = currentFormDTO.CurrentFormPermissionActions.EditHasPermission ? $"<button type='button' data-type='parent' style='margin:0px 5px 0 5px' data-parentid='{0}' data-id='{question.ID}' data-text='{question.Text}' data-order='{question.OrderNo}' data-operation='edit' data-AnswerCategoriesIds='{QuestionAnswersCategoriesIds(question)}'   id='btnEdit_{question.ID}' class='btn btn-warning' onclick='SaveQuestionlModal(this)' >" +
                "<i class='fas fa-edit'></i>" +
                "</button>"  : "";

            string delete = currentFormDTO.CurrentFormPermissionActions.DeleteHasPermission ? $"<button type='button' data-type='parent' style='margin:0px 5px 0 5px' data-parentid='{0}' data-id='{question.ID}'   id='btnDelete_{question.ID}' class='btn btn-danger' onclick='DeleteQuestion(this)' >" +
                "<i class='fas fa-trash'></i>" +
                "</button>"  : "";

            string arrange = "";
            if (!(question.IsLast ?? false))
                arrange = currentFormDTO.CurrentFormPermissionActions.EditHasPermission ? $"<button type='button' data-id='{question.ID}' data-text=' اسئلة \"{question.Text}\"' class='btn btn-warning' style='margin:0px 5px 0 5px' onclick='ShowOrderedQuestions(this)'><i class='fas fa-sort-numeric-up'></i></button>" : "";
            return $"<div class='add float-left-tree'>{ arrange+add + edit + delete}</div>";
                
           
        }

        private string AppendChildOperationBotton(Question question)
        {

            string islast = (question.IsLast??false) ? "islast":"";
            string add = currentFormDTO.CurrentFormPermissionActions.AddHasPermission ?
                $"<button type='button' data-type='child' data-parentid='{question.ParentId}'  data-id='{question.ID}' data-text='{question.Text}' data-operation='add'  id='btnAdd_{question.ID}'  class='btn btn-primary' onclick='SaveQuestionlModal(this)' >" +
            "<i class='fas fa-plus'></i>" +
            "</button>" : "";

            string edit = currentFormDTO.CurrentFormPermissionActions.EditHasPermission ?
                $"<button type='button' data-type='child' style='margin:0px 5px 0 5px' data-parentId='{question.ParentId}'  data-id='{question.ID}' data-text='{question.Text}' data-order='{question.OrderNo}' data-operation='edit'  data-AnswerCategoriesIds='{QuestionAnswersCategoriesIds(question)}'   id='btnEdit_{question.ID}' class='btn btn-warning' onclick='SaveQuestionlModal(this)' >" +
                "<i class='fas fa-edit'></i>" +
                "</button>" : "";

            string delete = currentFormDTO.CurrentFormPermissionActions.DeleteHasPermission ? 
                $"<button type='button' data-type='child' style='margin:0px 5px 0 5px' data-parentId='{question.ParentId}'  data-id='{question.ID}'  id='btnDelete_{question.ID}' class='btn btn-danger' onclick='DeleteQuestion(this)' >" +
                "<i class='fas fa-trash'></i>" +
                "</button>" : "";

            string arrange = "";
            if (!(question.IsLast??false))
                arrange = currentFormDTO.CurrentFormPermissionActions.EditHasPermission ? $"<button type='button' data-id='{question.ID}' data-text=' اسئلة \"{question.Text}\"' class='btn btn-warning' style='margin:0px 5px 0 5px' onclick='ShowOrderedQuestions(this)'><i class='fas fa-sort-numeric-up'></i></button>" : "";
            return $"<div class='add float-left-tree {islast}'>{ arrange + add + edit + delete}</div>";


        }

        private string QuestionAnswersCategoriesIds(Question question)
        {
            //try
            //{
            //    return question.Answers.Select(p => p.AnswerCategory).Select(p => p.ID.ToString()).Aggregate((a, b) => a + "," + b);
            //}
            //catch
            //{ }
            return String.Empty;
        }

        #endregion

        #endregion
        #region Actions

       public ResultViewModel DeleteQuestionFromTreeView(Guid questionId)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message= AppConstants.Messages.DeletedFailed};

            if (_repoQuestion.GetAllAsNoTracking().Where(p => p.ParentId == questionId).Any())
            {
                resultViewModel.Message = AppConstants.Messages.QuestionHasChildrens;
            }
           
            else if (_repoAnswer.GetAllAsNoTracking().Where(p => p.QuestionId==questionId).Any())
            {
                resultViewModel.Message = AppConstants.Messages.QuestionHasAnswers;
            }
            else if (_repoQuestion.GetAllAsNoTracking().Include(p => p.Answers).ThenInclude(p => p.VisitAnswerOptions).Where(p => p.Answers.Any(a => a.QuestionId == questionId && a.VisitAnswerOptions.Any())).Any())
            {
                resultViewModel.Message = AppConstants.Messages.QuestionHasRatings;
            }
            else
            {
                var tbl = _repoQuestion.GetById(questionId);
                tbl.ModifiedBy = tbl.DeletedBy = _repoQuestion.UserId;
                tbl.ModifiedDate = tbl.DeletedDate = AppDateTime.Now;
                tbl.IsDeleted = true;
                if (!_repoQuestion.Update(tbl))
                {
                    resultViewModel.Message = AppConstants.Messages.DeletedFailed;

                }
                else
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.DeletedSuccess;
                }
            }
            return resultViewModel;
        
        }
        public ResultViewModel Save(QuestionSaveDTO questionDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.DeletedFailed };
          
            var data = _repoQuestion.GetAllAsNoTracking().Where(p => p.ID == questionDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoQuestion.GetAllAsNoTracking().Where(p => p.ID != data.ID&& !p.IsDeleted  && p.Text.Trim().ToLower() == questionDTO.Text.Trim().ToLower() && p.ParentId == questionDTO.ParentId).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var tbl = _mapper.Map<Question>(questionDTO);

                tbl.ModifiedDate = AppDateTime.Now;
                tbl.ModifiedBy = _repoQuestion.UserId;
                if (_repoQuestion.Update(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoQuestion.GetAllAsNoTracking().Where(p =>  !p.IsDeleted&&p.Text.Trim().ToLower() == questionDTO.Text.Trim().ToLower() && p.ParentId == questionDTO.ParentId).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
               
                var tbl = _mapper.Map<Question>(questionDTO);
                if (questionDTO.ParentId.HasValue)
                {
                   tbl.IsLast = questionDTO.AnswersCategoriesIds.Count>0;


                }
                tbl.AddedBy = _repoQuestion.UserId;
                tbl.HasMultiCategoryAnswer = true;
                tbl.ID = Guid.NewGuid();
                if (questionDTO.ParentId.HasValue)
                {
                     Check(questionDTO.ParentId);


                    var parentTbl = _repoQuestion.GetAllAsNoTracking().Where(p => p.ID == questionDTO.ParentId).FirstOrDefault();
                    if (parentTbl != null)
                    {
                        if (parentTbl.QuestionParentNo != count || parentTbl.IsLast == false)
                        {
                            if (parentTbl.ParentId.HasValue)
                            {


                                if (count == 3)
                                    parentTbl.QuestionParentNo = null;

                                else
                                    parentTbl.QuestionParentNo = count;

                                parentTbl.IsLast = false;
                                if(questionDTO.AnswersCategoriesIds.Count()==0)
                                tbl.QuestionParentNo = count + 1;

                                _repoQuestion.UpdateWithoutSaveChange(parentTbl);
                            }
                        }


                    }
                }
                if (questionDTO.AnswersCategoriesIds.Count > 0)
                {
                    int index = 1;

                    foreach (var answerCategoryId in questionDTO.AnswersCategoriesIds)
                    {
                        var answersTemplates = _repoAnswerCategory.GetAllAsNoTracking().Include(p => p.AnswerTemplates).Where(p => !p.IsDeleted && p.IsActive && p.ID == answerCategoryId).SelectMany(p => p.AnswerTemplates);
                        var answers = answersTemplates.Select(p => new Answer
                        {
                            QuestionId = tbl.ID,
                            Text = p.Text,
                            AddedBy = tbl.AddedBy,
                            AnswerCategoryId = p.AnswerCategoryId,
                            OrderNo = p.OrderNo,
                            AnswerCategoryOrderNo = (index),


                        });
                        index++;
                        _repoAnswer.InsertRangeWithoutSaveChange(answers);

                    }

                   ;

                }
                _repoQuestion.InsertWithoutSaveChange(tbl);
                if (_repoQuestion.SaveChange())
                {

                //    _repoAnswer.SaveChange();

                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }

            return resultViewModel;

        }

        public ResultViewModel SaveQuestionsOrder(List<QuestionsOrderDTO> mdl)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message=AppConstants.Messages.SavedFailed};

            mdl.ForEach(question =>
            {
                var tbl = _repoQuestion.GetAllAsNoTracking().Where(p => p.ID == question.Id).FirstOrDefault();
                if (tbl!=null)
                {
                    tbl.OrderNo = question.OrderNo;
                    _repoQuestion.UpdateWithoutSaveChange(tbl);

                }
            });
            

            if (_repoQuestion.SaveChange())
            {
                resultViewModel.Status = true;
                resultViewModel.Message = AppConstants.Messages.SavedSuccess;

            }
            return resultViewModel;
        }


        private  int count = default;
        public ResultViewModel CheckAddChild(Guid questionId)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.DeletedFailed };
            var data = _repoQuestion.GetAllAsNoTracking().Where(p=>p.ID==questionId).FirstOrDefault();

            if (data!=null)
            {
                if (data.ParentId.HasValue)
                {
                    count++;
                  Check(data.ParentId);

                }
            }
            //1,2 safe
            //3 no
            resultViewModel.Status = count < 3;
            resultViewModel.Message = count < 3 ? "" : AppConstants.Messages.QuestionHas3Level;
            resultViewModel.Data = count;
            count = default;
            return resultViewModel;

        }
        public int Check(Guid? questionId)
        {
            var data = _repoQuestion.GetAllAsNoTracking().Where(p => p.ID == questionId).FirstOrDefault();
            if (data != null)
            {
                if (data.ParentId.HasValue)
                {
                    count++;
                   Check(data.ParentId);

                }
                return 1;
               
            }
            return count;
        }
        #endregion

    }


}
