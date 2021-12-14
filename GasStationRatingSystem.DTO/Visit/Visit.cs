using GasStationRatingSystem.Common.General;
using GasStationRatingSystem.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class CreateVisitDTO
    {
      //  [Required(ErrorMessage = nameof(GasStationRatingSystemResources.EnvironmentalSpecialistRequired))]

        public Guid? UserId { get; set; } = null;
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.StationRequired))]

        public Guid? StationId { get; set; } = null;
        public bool FromVisitApproval { get; set; } = false;
    }
    public class SaveVisitAnswersDTO
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.VisitIdRequired))]

        public Guid? VisitId { get; set; }

        public int PageIndex { get; set; }
        public StationStatus StationStatusNo { get; set; }
        public List<OptionsDTO> Options { get; set; }
        public List<QuestionsImagesDTO> QuestionsImages { get; set; }
        public SigntureDataDTO SigntureData { get; set; }
        public VisitNotesDataDTO VisitNotesData { get; set; }

    }
    public class SaveOfflineVisitAnswersDTO
    {
        [Required(ErrorMessage = nameof(GasStationRatingSystemResources.VisitIdRequired))]

        public List<SaveVisitAnswersDTO> SaveVisitsAnswers { get; set; }
    }
    public class OptionsDTO
    {
        public Guid? OptionId { get; set; }
        public Guid? RefId { get; set; }
    }
    public class QuestionsImagesDTO
    {
        public Guid? QuestionId { get; set; }
        public List<string> QuestionBase64Images { get; set; }
    }
    public class VisitNotesDataDTO
    {
        public string VisitNotes { get; set; }
        public List<string> VisitBase64Images { get; set; }
    }
    public class SigntureDataDTO
    {
        public string SigntureNotes { get; set; }
        public List<string> SigntureNotesBase64Images { get; set; }

        public string SigntureBase64Image { get; set; }
    }


    public class DeleteVisitAnswersDTO
    {
        public int RowsAffected { get; set; }
    }
}
