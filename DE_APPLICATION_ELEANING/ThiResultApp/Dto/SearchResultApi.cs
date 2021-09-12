using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiResultApp.Dto
{
   public class SearchResultApi
    {
        public Guid? StudentID { set; get; }
        public Guid? QuestionID { set; get; }
        public Guid? AnswerID { set; get; }
        public Guid? DeThiID { set; get; }
    }
}
