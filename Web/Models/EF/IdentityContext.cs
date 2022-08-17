using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Models.EF
{
    public class IdentityContext :IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions _options;

        public IdentityContext(DbContextOptions options) : base(options)
        {
            _options = options;
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
