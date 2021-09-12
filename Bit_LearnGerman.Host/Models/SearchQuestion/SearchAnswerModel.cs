using System;
using System.ComponentModel.DataAnnotations;

namespace Oauth_2._0_v2.Models.SearchQuestion
{
    public class SearchAnswerModel
    {
        [Required(ErrorMessage = "The type of QuestionId required")]
        public Guid QuestionId { set; get; }
        [Required(ErrorMessage = "The type of QuestionId required")]
        public Guid AnswerId { set; get; }
    }
}