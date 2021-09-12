using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
namespace DE_DB_ELEANING.Model
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime LastActive { get; set; } = DateTime.Now;
        public DateTime DayOfBirth { get; set; }
        public string DisplayName { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
