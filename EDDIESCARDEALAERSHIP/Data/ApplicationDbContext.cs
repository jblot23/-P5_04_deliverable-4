using EDDIESCARDEALAERSHIP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EDDIESCARDEALAERSHIP.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; } // Define a DbSet<Car> property for the Car entity
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
