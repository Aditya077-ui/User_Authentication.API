using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User_Authentication.API.Model;

namespace User_Authentication.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext>options):base(options) 
        {
            
        }

        public DbSet<ApplicationUser> applicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var AdminRoleId = "a32a4c45-5184-4ca3-8518-96e1ba46cabb";
            var UserRoleId = "242cc943-a59e-444b-9de2-34b67053c645";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                     Id = AdminRoleId,
                     ConcurrencyStamp = AdminRoleId,
                     Name = "Admin",
                     NormalizedName = "Admin".ToUpper(),

                },
                new IdentityRole()
                {
                    Id = UserRoleId,
                    ConcurrencyStamp= UserRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);


        }
    }
}
