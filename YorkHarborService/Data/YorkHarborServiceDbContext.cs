using YorkHarborService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace YorkHarborService.Data
{
    public class YorkHarborServiceDbContext : IdentityDbContext<IdentityUser>
    {
		public YorkHarborServiceDbContext(DbContextOptions options):base(options)
		{
		}
		public DbSet<Review> Reviews { get; set; }
        public DbSet<Employee> Employees { get; set; }
	}
}
