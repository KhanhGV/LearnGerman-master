using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.ThiBaiTapApp.Dto
{
    public class SearchThiBaiTap
    {
        public SearchThiBaiTap()
        {

        }

        public SearchThiBaiTap(Guid? _ID)
        {
            ID = _ID;
        }

        //public SearchThiBaiTap(bool status)
        //{
        //    Status = status;
        //}

        public Guid? ID { get; set; }
        public string BtNameVi { get; set; }
        public string BtNameDe { get; set; }
        public Guid? DeThiID { get; set; }
        public Guid? SubjectTypeId { get; set; }
        public virtual ICollection<ThiQuestion> ThiQuestions { get; set; }
    }
}
