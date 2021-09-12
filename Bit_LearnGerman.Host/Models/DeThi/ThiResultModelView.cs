using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Oauth_2._0_v2.Models.DeThi
{
    public class ThiResultModelView
    {
        [Required]
        public Guid QuestionID { set; get; }
        [Required]
        public Guid DeThiID { set; get; }
        [Required]
        public List<ThiResultView> ListResult;

        //public Guid StudentID { set; get; }

    }
    public class ThiResultView
    {

        [Required]
        public Guid AnswerID { set; get; }

        [Required]
        public int Sequence { set; get; }
        public string AnswerContent { set; get; }
        public bool? IsTrue { set; get; }
    }
}