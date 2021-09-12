using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oauth_2._0_v2.Models.Template
{
    public class SearchTemplateModel
    {
        [Required(ErrorMessage = "The type of subjectId required")]
        public Guid subjectId { set; get; }
    }
}