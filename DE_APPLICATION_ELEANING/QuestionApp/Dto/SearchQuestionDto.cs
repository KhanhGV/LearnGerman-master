using System;
namespace DE_APPLICATION_ELEANING.QuestionApp.Dto
{
    public class SearchQuestionDto
    {
        public SearchQuestionDto(Guid? questionTypeId, Guid? wordId)
        {
            QuestionTypeId = questionTypeId;
            WordId = wordId;
        }
        public SearchQuestionDto()
        {

        }
        public string NameDe { set; get; }
        public string NameVi { set; get; }
        public Guid? QuestionTypeId { set; get; }
        public Guid? SubjectId { set; get; }
        public Guid? FormatQuestion { set; get; }
        public Guid? Grammar_TheoryId { set; get; }
        public Guid? CommunicationId { set; get; }
        public Guid? WordId { set; get; }
        public bool? Status { set; get; }
        public string  type { set; get; }
    }
}
