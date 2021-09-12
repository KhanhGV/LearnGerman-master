using System;
using System.ComponentModel.DataAnnotations;
namespace Oauth_2._0_v2.Models.SearchQuestion
{
    public class SearchQuestionModel
    {
        [Required(ErrorMessage= "Need to have a topic")]
        public Guid SubjectId { set; get; }
        [Required(ErrorMessage = "The type of question required")]
        public Guid QuestionType { set; get; }
    }
}