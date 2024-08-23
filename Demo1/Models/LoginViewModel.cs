using System.ComponentModel.DataAnnotations;

namespace Demo1.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; } // Role to determine if the user is a student or a tutor
    }

}
