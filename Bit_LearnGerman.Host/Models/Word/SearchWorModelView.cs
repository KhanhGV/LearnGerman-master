using System;
using System.ComponentModel.DataAnnotations;
namespace Oauth_2._0_v2.Models.Word
{
    public class SearchWorModelView
    {
        public Guid?  SubJectId { set; get; }
        public string Name { set; get; }
        public int? PageNumber { set; get; }
    }
}