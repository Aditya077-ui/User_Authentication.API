using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace User_Authentication.API.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        public string? DepartmentName { get; set; }

        public string? City { get; set; }

        public string? State { get;set; }

       
    }
}
