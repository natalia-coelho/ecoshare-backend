using System.ComponentModel.DataAnnotations;
using ecoshare_backend.Models;

namespace ecoshare_backend.Data.DTOs
{
    public class UserRegistrationDTO
    {
        public UserRegistrationDTO() { }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; } = string.Empty;

        [Required]
        public UserRole Roles { get; set; }
    }
}
