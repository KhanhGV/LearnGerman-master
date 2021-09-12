using System.ComponentModel.DataAnnotations;
namespace Oauth_2._0_v2.Models
{
    public class SingupModel
    {
        [Required]
        public string FullName { set; get; }
        [Required]
        [MaxLength(30)]
        [MinLength(9)]
        public string PhoneNumber { set; get; }
        [Required]
        public string EmailAddress { set; get; }
        public string LinkFacebook { set; get; }
        [Required]
        [MaxLength(8)]
        [MinLength(6)]
        public string PassWord { set; get; }
        [MaxLength(16)]
        [MinLength(6)]
        [Required]
        public string RePassWord { set; get; }
    }
}