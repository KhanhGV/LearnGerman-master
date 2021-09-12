using DE_DB_ELEANING.AFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE_APPLICATION_ELEANING.DeThiApp.Dto
{
    public class SearchDeThi
    {
        public SearchDeThi()
        {

        }

        public SearchDeThi(Guid _ID)
        {
            ID = _ID;
        }

        public SearchDeThi(bool status)
        {
            Status = status;
        }

        public Guid? ID { get; set; }
        public int Index { get; set; }
        public string NameDe { get; set; }
        public string NameVi { get; set; }
        public bool? Status { get; set; }

        public string Type { get; set; }
        public virtual ICollection<ThiQuestion> ThiQuestions { get; set; }
    }
}
