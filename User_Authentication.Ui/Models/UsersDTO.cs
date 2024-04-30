using System.ComponentModel.DataAnnotations;

namespace User_Authentication.Ui.Models
{
    public class UsersDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(25, ErrorMessage = "UserName should be maximum of 25 length")]
        public string UserName { get; set; }

        public string DepartmentName { get; set; }

        public string[] Roles { get; set; }
    }
}
