using System.ComponentModel.DataAnnotations;

namespace User_Authentication.Web.Models
{
    public class RegisterDto
    {
        public string Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(25, ErrorMessage = "UserName should be maximum of 25 length")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }


        public string DepartmentName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Required]
        public string[] Roles { get; set; }
    }
}
