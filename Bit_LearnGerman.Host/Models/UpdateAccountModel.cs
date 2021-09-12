using System.ComponentModel.DataAnnotations;
using System.Web;
namespace Oauth_2._0_v2.Models
{
    public class UpdateAccountModel
    {
        public string FullName { set; get; }
        [MaxLength(10)]
        [MinLength(9)]
        public string PhoneNumber { set; get; }
        public string EmailAddress { set; get; }
        public string LinkFacebook { set; get; }
        [MaxLength(8)]
        [MinLength(6)]
        public string OldPassWord { set; get; }
        [MaxLength(8)]
        [MinLength(6)]
        public string NewPassWord { set; get; }
    }
}