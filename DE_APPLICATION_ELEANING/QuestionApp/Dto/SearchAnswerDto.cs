using System;
namespace DE_APPLICATION_ELEANING.QuestionApp.Dto
{
    public class SearchAnswerDto
    {
        public SearchAnswerDto()
        {
                
        }
        public SearchAnswerDto(Guid studenId, Guid questionId,Guid wordId)
        {
            StudenId = studenId;
            QuestionId = questionId;           
            WordId = wordId;
        }

        public Guid StudenId { set; get; }
        public Guid QuestionId { set; get; }
        
        public Guid ImgId { set; get; }
        public Guid WordId { set; get; }
        public bool? Status { set; get; }
        public string Type { set; get; }
    }
}
