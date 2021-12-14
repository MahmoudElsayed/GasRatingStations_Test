using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GasStationRatingSystem.DTO
{
    public class QuestionsOrderDTO
    {
        public Guid Id { get; set; }

        public string Text { get; set; }
        public int OrderNo { get; set; }
    }
    public class ParentQuestionDTO
    {
        public int PageIndex { get; set; }
        public string ParentItemText { get; set; }
        public string ItemOneText { get; set; }

        public ItemsDTO Items { get; set; }
    }
    public class ItemsDTO
    {
        public string ItemTwoText { get; set; }

        public List<QuestionsDTO> Questions { get; set; }
    }
    public class QuestionsDTO
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public int QuestionTypeNo { get; set; }
        public bool? MultipleAnswerCategory { get; set; }
        public object ListOfReference { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
    public class QuestionSaveDTO
    {
        public Guid ID { get; set; }
        public Guid? ParentId { get; set; }

        public string Text { get; set; }
        public int OrderNo { get; set; }
        [NotMapped]
        public List<Guid> AnswersCategoriesIds { get; set; } = new List<Guid>();
    }
    public class AnswerDTO
    {
        public Guid AnswerCategoryId { get; set; }
        public string Label { get; set; }
        public List<OptionsAnswerDTO> Options { get; set; }
    }
    public class OptionsAnswerDTO
    {
        public Guid OptionId { get; set; }
        public string OptionText { get; set; }
        public bool IsSelected { get; set; }

    }

    public class GetQusetionIdDTO
    {
        public Guid QuestionId { get; set; }
        public int OrderNo { get; set; }
    }
    public class QuestionsTreeViewDTO
    {
        public Guid Id { get; set; }
        public object State { get; set; }
        public string Text { get; set; }
        public List<QuestionsTreeViewDTO> Nodes { get; set; }
    }
}
