using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.SubjectApp.Dto
{
    public class SearchSubjectDto
    {
        public SearchSubjectDto(string nameDe, string nameVi, Guid? type)
        {
            NameDe = nameDe;
            NameVi = nameVi;
            Type = type;
        }
        public SearchSubjectDto()
        {

        }

        public SearchSubjectDto(Guid? type)
        {
            Type = type;
        }

        public string NameDe { set; get; }
        public string NameVi { set; get; }
        public Guid? Type { set; get; }
        public bool? Status { set; get; }
    }
}
