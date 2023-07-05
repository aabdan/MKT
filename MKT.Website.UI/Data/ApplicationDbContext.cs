using Microsoft.EntityFrameworkCore;
using MKT.Website.Models;
using MKT.Website.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace MKT.Website.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
               new { Id = "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", Name = "Admin", NormalizedName = "ADMIN" }
               );

            ApplicationUser user = new ApplicationUser()
            {
                Email = "SuperAdmin@gmail.com",
                UserName = "SuperAdmin",
                NormalizedUserName = "SUPERADMIN",
                NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                EmailConfirmed = true
            };
            var password = "Admin1234#";
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var hashedPassword = passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            modelBuilder.Entity<IdentityUserRole<string>>()
                     .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = user.Id, RoleId = "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7" });
        }
    }
}
