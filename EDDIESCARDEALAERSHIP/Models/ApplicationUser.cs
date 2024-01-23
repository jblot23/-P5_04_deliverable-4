using Microsoft.AspNetCore.Identity;

namespace EDDIESCARDEALAERSHIP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool isAccess { get; set; }
    }

}
