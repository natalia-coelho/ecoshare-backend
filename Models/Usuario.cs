using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ecoshare_backend.Models
{
    public class Usuario : IdentityUser
    {
        public DateTime DataNascimento { get; set; }
        public UserRole Role { get; set; } = UserRole.CLIENT;

        // construtor que vai invocar o identity user através do base()
        public Usuario() : base() 
        {

        }
    }
}
