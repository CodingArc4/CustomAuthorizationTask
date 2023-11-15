using CustomAuthorizationTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomAuthorizationTask.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<SubCatagory> SubCatagories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedPermissions(modelBuilder);
        }

        private static void SeedPermissions(ModelBuilder modelBuilder)
        {
            var adminRoleId = "c8627e22-e1b9-406d-9f54-39c801e2d1df";
            var userRoleId = "8dd18f55-c196-4835-8fba-c8892d35a242";

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(
               new IdentityRoleClaim<string> { Id = 1, RoleId = adminRoleId, ClaimType = "Permission", ClaimValue = Permissions.Permissions.Products.View },
               new IdentityRoleClaim<string> { Id = 2, RoleId = adminRoleId, ClaimType = "Permission", ClaimValue = Permissions.Permissions.Products.Create},
               new IdentityRoleClaim<string> { Id = 3, RoleId = userRoleId, ClaimType = "Permission", ClaimValue = Permissions.Permissions.Catagory.View}
           );
        }
    }
}
