using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiAnswerApp.Dto
{    
    public class SearchThiAnswer
    {
        public SearchThiAnswer()
        {

        }

        public SearchThiAnswer(Guid _ID)
        {
            ID = _ID;
        }

        public SearchThiAnswer(bool status)
        {
            Status = status;
        }

        public Guid ID { get; set; }
        public int Index { get; set; }
        public bool Status { get; set; }
        public Guid QuestionID { get; set; }
    }
}
