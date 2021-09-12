using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiQuestionApp.Dto
{
   public class SearchThiQuestionApi
    {                      
        public Guid? BaiTapThiID { get; set; }
        public Guid? ThiTypeSubjectID { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
