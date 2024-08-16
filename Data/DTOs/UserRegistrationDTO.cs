using System.ComponentModel.DataAnnotations;
using ecoshare_backend.Models;

namespace ecoshare_backend.Data.DTOs
{
    public class UserRegistrationDTO
    {
        public UserRegistrationDTO() { }

        [Required]
        public string Nome { get; set; } = string.Empty; 

        [Required]
        public string Sobrenome { get; set; } = string.Empty; 
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; } = string.Empty;

        [Required]
        public int Role { get; set; }
        public UserRole? RoleObject { get; set; }
    }
}
