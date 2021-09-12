using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oauth_2._0_v2.Models.DeThi
{
    public class SearchBaiTapThiModelView
    {
        [Required]
        public Guid? DeThiID { get; set; }
        [Required]
        public Guid? SubjectTypeID { get; set; }
    }
}