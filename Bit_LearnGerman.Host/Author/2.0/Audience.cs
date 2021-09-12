using System.ComponentModel.DataAnnotations;

namespace Oauth_2._0_v2.Author._2._0
{
    public class Audience
    {
        [Key]
        [MaxLength(32)]
        public string ClientId { get; set; }
        [MaxLength(80)]
        [Required]
        public string Base64Secret { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}