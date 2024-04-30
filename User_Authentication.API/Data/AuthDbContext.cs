using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace User_Authentication.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext>options):base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var AdminRoleId = "b64ed0f6-b469-46ad-97ac-c18d5129d115";
            var NormalUserRoleId = "a82891a8-015b-4e44-b6cb-a51935b8fce0";

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
             Id = NormalUserRoleId,
             ConcurrencyStamp= NormalUserRoleId,
             Name = "User",
             NormalizedName = "User".ToUpper()
         }
     };

            builder.Entity<IdentityRole>().HasData(roles);

        }

    }
}
