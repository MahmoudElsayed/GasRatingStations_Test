using AutoMapper;
using GasStationRatingSystem.Common;
using GasStationRatingSystem.DAL;
using GasStationRatingSystem.DTO;
using GasStationRatingSystem.DTO.Guide;
using GasStationRatingSystem.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GasStationRatingSystem.BLL
{
    public class MainInfluenceBll
    {
        #region Fields
        private const string _spMainInfluencs = "Guide.[spMainInfluencs]";

        private readonly IRepository<MainInfluence> _repoMainInfluence;
        private readonly IRepository<MainInfluenceQuestion> _repoMainInfluenceQuestion;

        private readonly IMapper _mapper;
        public MainInfluenceBll(IRepository<MainInfluence> repoMainInfluence, IRepository<MainInfluenceQuestion> repoMainInfluenceQuestion, IMapper mapper)
        {
            _repoMainInfluence = repoMainInfluence;
            _repoMainInfluenceQuestion = repoMainInfluenceQuestion;
            _mapper = mapper;
        }

        #endregion
        #region Get


        public MainInfluence GetById(Guid id)
        {

            return _repoMainInfluence.GetAllAsNoTracking().Where(p => p.ID == id).Include(p=>p.MainInfluenceQuestions)
                .Select(p => new MainInfluence() { ID = p.ID, Name = p.Name , MainInfluenceQuestions=p.MainInfluenceQuestions}).FirstOrDefault();
        }
        public IQueryable<SelectListDTO> GetSelect()
        {

            return _repoMainInfluence.GetAllAsNoTracking().Where(p => !p.IsDeleted && p.IsActive)

                .Select(p => new SelectListDTO()
                {
                    Value = p.ID,
                    Text = p.Name
                });
        }
        public DataTableResponse LoadData(DataTableRequest mdl)
        {
            var data = _repoMainInfluence.ExecuteStoredProcedure<MainInfluenceTableDTO>
                (_spMainInfluencs, mdl?.ToSqlParameter(), CommandType.StoredProcedure);

            return new DataTableResponse() { AaData = data, ITotalRecords = data?.FirstOrDefault()?.TotalCount ?? 0 };
        }
        #endregion
        public ResultViewModel Save(MainInfluenceDTO MainInfluenceDTO)
        {
            ResultViewModel resultViewModel = new ResultViewModel() { Message = AppConstants.Messages.SavedFailed };


            var data = _repoMainInfluence.GetAllAsNoTracking().Where(p => p.ID == MainInfluenceDTO.ID).FirstOrDefault();
            if (data != null)
            {

                if (_repoMainInfluence.GetAllAsNoTracking().Where(p => p.ID != data.ID && p.Name.Trim().ToLower() == MainInfluenceDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }

                var tbl = _mapper.Map<MainInfluence>(MainInfluenceDTO);

                tbl.ModifiedDate = AppDateTime.Now;
               
                if (_repoMainInfluence.Update(tbl))
                {
                    _repoMainInfluenceQuestion.ExecuteStoredProcedure<int>($"delete from [Guide].[MainInfluenceQuestions] where [MainInfluenceId]='{tbl.ID}'", null, CommandType.Text);

                    _repoMainInfluenceQuestion.InsertRange(MainInfluenceDTO.QuestionsIds.Select(p => new MainInfluenceQuestion()
                    {
                        AddedBy = _repoMainInfluenceQuestion.UserId,
                        MainInfluenceId = MainInfluenceDTO.ID,
                        QuestionId = p,

                    }).ToList());
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }
            else
            {
                if (_repoMainInfluence.GetAllAsNoTracking().Where(p => p.Name.Trim().ToLower() == MainInfluenceDTO.Name.Trim().ToLower()).FirstOrDefault() != null)
                {
                    resultViewModel.Message = AppConstants.Messages.NameAlreadyExists;
                    return resultViewModel;

                }
                var tbl = _mapper.Map<MainInfluence>(MainInfluenceDTO);
                tbl.MainInfluenceQuestions = MainInfluenceDTO.QuestionsIds.Select(p => new MainInfluenceQuestion()
                {
                    AddedBy = _repoMainInfluence.UserId,
                    MainInfluenceId = MainInfluenceDTO.ID,
                    QuestionId = p,

                }).ToList();
                if (_repoMainInfluence.Insert(tbl))
                {
                    resultViewModel.Status = true;
                    resultViewModel.Message = AppConstants.Messages.SavedSuccess;

                }
            }





            return resultViewModel;
        }

        public ResultViewModel Delete(Guid id)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            var tbl = _repoMainInfluence.GetAllAsNoTracking().Where(p => p.ID == id).FirstOrDefault();
            tbl.IsDeleted = true;
            tbl.DeletedBy = null;
            tbl.DeletedDate = AppDateTime.Now;
            var IsSuceess = _repoMainInfluence.Update(tbl);

            resultViewModel.Status = IsSuceess;
            resultViewModel.Message = IsSuceess ? AppConstants.Messages.DeletedSuccess : AppConstants.Messages.DeletedFailed;


            return resultViewModel;
        }
    }
}
