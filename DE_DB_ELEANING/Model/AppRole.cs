using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DE_DB_ELEANING.Model
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
