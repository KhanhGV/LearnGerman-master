using System;
namespace DE_APPLICATION_ELEANING.WordApp.Dto
{
    public class SearchWorDto
    {
        public string NameDe { set; get; }
        public string NameVi { set; get; }
        public Guid? SubjectId { set; get; }
        public bool? Status { set; get; }
    }

}
