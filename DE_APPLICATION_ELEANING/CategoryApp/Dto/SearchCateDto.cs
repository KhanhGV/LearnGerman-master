using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.CategoryApp.Dto
{
    public class SearchCateDto
    {
        public SearchCateDto()
        {

        }

        public SearchCateDto(string nameVi)
        {
            NameVi = nameVi;
        }

        public SearchCateDto(bool? status)
        {
            Status = status;
        }

        public string NameVi { set; get; }
        public string NameDe { set; get; }
        public bool? Status { set; get; }
    }
}
