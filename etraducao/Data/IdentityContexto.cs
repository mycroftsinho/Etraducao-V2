using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace etraducao.Data
{
    public class IdentityContexto : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration configuration;

        public IdentityContexto(DbContextOptions<IdentityContexto> options, IConfiguration configuration) : base(options)
        {
            this.Database.SetCommandTimeout(100);
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users", "auth");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles", "auth");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles", "auth");

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims", "auth");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins", "auth");

            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims", "auth");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("UserToken", "auth");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=etraducao2.azurewebsites.net;Integrated Security=True;Connection Timeout=30;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
