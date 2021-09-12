using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.SubjectTypeApp.Dto
{  
    public class SearchSubjectType
    {
        public SearchSubjectType(string nameDe, string nameVi, Guid? type)
        {
            NameDe = nameDe;
            NameVi = nameVi;
            Type = type;
        }
        public SearchSubjectType()
        {

        }

        public SearchSubjectType(Guid? type)
        {
            Type = type;
        }

        public string NameDe { set; get; }
        public string NameVi { set; get; }
        public Guid? Type { set; get; }
        public bool? Status { set; get; }
    }
}
