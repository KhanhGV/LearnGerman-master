using System;
using System.ComponentModel.DataAnnotations;
namespace Oauth_2._0_v2.Models.SearchQuestion
{
    public class CompleteExcesiseModel
    {
        [Required(ErrorMessage = "Need to have a topic")]
        public Guid ExexerciseId { set; get; }
      
    }
}