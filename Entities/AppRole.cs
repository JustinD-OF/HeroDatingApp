using Microsoft.AspNetCore.Identity;

namespace HeroDatingApp.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}