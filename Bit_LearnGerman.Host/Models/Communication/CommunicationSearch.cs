using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oauth_2._0_v2.Models.Communication
{
    public class CommunicationSearch
    {
        [Required(ErrorMessage = "The type of CommunicationId required")]
        public Guid CommunicationId { set; get; }
    }
}