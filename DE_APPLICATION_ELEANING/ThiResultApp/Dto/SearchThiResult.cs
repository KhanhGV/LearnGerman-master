using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiResultApp.Dto
{

    public class SearchThiResult
    {
        public SearchThiResult()
        {

        }

        public SearchThiResult(Guid? _ID)
        {
            ID = _ID;
        }

        public SearchThiResult(Guid? studentID, Guid? questionID, Guid? answerID, Guid? deThiID, string type)
        {
            StudentID = studentID;
            QuestionID = questionID;
            AnswerID = answerID;
            DeThiID = deThiID;
            Type = type;
        }

        public Guid? ID { set; get; }
        public Guid? StudentID { set; get; }
        public Guid? QuestionID { set; get; }
        public Guid? AnswerID { set; get; }
        public Guid? DeThiID { set; get; }
        public string Type { set; get; }
        //public SearchThiQuestionType(bool status)
        //{
        //    Status = status;
        //}

        //public Guid? ID { get; set; }
        //public int Index { get; set; }
        //public bool Status { get; set; }
    }
}
