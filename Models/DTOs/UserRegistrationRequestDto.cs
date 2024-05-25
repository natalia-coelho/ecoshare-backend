using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models.DTOs
{
    public class UserRegistrationRequestDto
    {
        public UserRegistrationRequestDto() { }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
