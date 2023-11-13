using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reservation.Models;

namespace Reservation.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string,
        IdentityUserClaim<string>,AppUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(b =>
            {
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            builder.Entity<AppRole>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            Seed(builder);
        }

        private void Seed(ModelBuilder builder)
        {
            var rolAdmin = new AppRole()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var roleEditor = new AppRole()
            {
                Name = "Editor",
                NormalizedName = "EDITOR"
            };
            builder.Entity<AppRole>().HasData(rolAdmin, roleEditor);

        }
    }
}