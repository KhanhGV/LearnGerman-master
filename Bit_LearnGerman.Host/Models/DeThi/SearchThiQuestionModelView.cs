using System;
using System.ComponentModel.DataAnnotations;
namespace Oauth_2._0_v2.Models.DeThi
{
    public class SearchThiQuestionModelView
    {
        [Required]
        public Guid BaiTapThiID { get; set; }
        //[Required]
        //public Guid ThiTypeSubjectID { get; set; }
    }
}