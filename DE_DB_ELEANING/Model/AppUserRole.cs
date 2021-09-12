using Microsoft.AspNet.Identity.EntityFramework;

namespace DE_DB_ELEANING.Model
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
    }
}
