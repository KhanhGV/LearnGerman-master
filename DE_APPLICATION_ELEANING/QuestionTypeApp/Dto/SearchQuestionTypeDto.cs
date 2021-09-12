using System;

namespace DE_APPLICATION_ELEANING.QuestionTypeApp.Dto
{
    public class SearchQuestionTypeDto
    {
        public SearchQuestionTypeDto(Guid? subjectTypeId)
        {
            SubjectTypeId = subjectTypeId;
        }
        public SearchQuestionTypeDto()
        {

        }
        public string NameDe { set; get; }
        public string NameVi { set; get; }
        public Guid? SubjectTypeId { set; get; }
        public bool? Status { set; get; }
    }
}
