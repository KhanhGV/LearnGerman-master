using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.TemplateApp.Dto
{
    public class TemplateSearchDto
    {
        public string NameDe { set; get; }
        public string NameVi { set; get; }
        public Guid? SubjectId { set; get; }
        public bool? Status { set; get; }
    }
}
