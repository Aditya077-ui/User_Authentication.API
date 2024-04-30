using System.ComponentModel.DataAnnotations;

namespace User_Authentication.API.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(25, ErrorMessage = "UserName should be maximum of 25 length")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
