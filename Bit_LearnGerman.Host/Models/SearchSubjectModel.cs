using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oauth_2._0_v2.Models
{
    public class SearchSubjectModel
    {
        public string NameDe { set; get; }
        public string NameVi { set; get; }
        [Required(ErrorMessage = "Menu is required")]
        public Guid? Menu { set; get; }
    }
}