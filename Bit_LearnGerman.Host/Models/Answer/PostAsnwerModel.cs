using System;
using System.ComponentModel.DataAnnotations;

namespace Oauth_2._0_v2.Models.Answer
{
    public class PostAsnwerModel
    {
        [Required(ErrorMessage = "The type of QuestionId required")]
        public Guid QuestionId { set; get; }
        [Required(ErrorMessage = "The type of YourWord required")]
        public string YourWord { set; get; }
    }
}