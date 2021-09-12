using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiQuestionApp.Dto
{
    public class SearchThiQuestion
    {
        public SearchThiQuestion()
        {

        }

        public SearchThiQuestion(Guid? _ID)
        {
            ID = _ID;
        }

        public SearchThiQuestion(bool status)
        {
            Status = status;
        }

        public Guid? ID { get; set; }
        public int Index { get; set; }
        public bool Status { get; set; }
        public Guid? BaiTapThiID { get; set; }        
        public string TypeSearch { get; set; }
        public virtual ICollection<ThiAnswer> ThiAnswers { get; set; }
    }
}
