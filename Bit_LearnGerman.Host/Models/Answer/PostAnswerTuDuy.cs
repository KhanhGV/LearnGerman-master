using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oauth_2._0_v2.Models.Answer
{
    public class PostAnswerTuDuy
    {
        [Required(ErrorMessage = "The type of QuestionId required")]
        public Guid QuestionId { set; get; }

        public List<AnswerTuDuy> ListResult;
    }
    public class AnswerTuDuy
    {
        [Required(ErrorMessage = "The type of WordId required")]
        public Guid WordId { set; get; }
        [Required(ErrorMessage = "The type of ImgId required")]
        public Guid ImgId { set; get; }
    }
}