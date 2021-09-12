using System;
using System.ComponentModel.DataAnnotations;

namespace Oauth_2._0_v2.Models.SearchQuestion
{
    public class SearchQuestionGrammarModel
    {
        [Required(ErrorMessage = "The type of ExerciseId required")]
        public Guid ExerciseId { set; get; }
    }
}