using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiQuestionTypeApp.Dto
{    
    public class SearchThiQuestionType
    {
        public SearchThiQuestionType()
        {

        }

        public SearchThiQuestionType(Guid? _ID)
        {
            ID = _ID;
        }

        public SearchThiQuestionType(bool status)
        {
            Status = status;
        }

        public Guid? ID { get; set; }
        public int Index { get; set; }
        public bool Status { get; set; }       
    }
}
